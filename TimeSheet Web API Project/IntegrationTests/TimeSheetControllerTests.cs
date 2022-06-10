using FluentAssertions;
using TimeSheet.DTO_Models;
using TimeSheet.Models;

namespace IntegrationTests
{
    [TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]
    public class TimeSheetControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public TimeSheetControllerTests(TestingWebAppFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact, TestPriority(1)]
        public async void TimeSheetController_Get_ReturnsTimeSheetList()
        {
            // Arrange

            // Act
            var response = await _httpClient.GetAsync("api/TimeSheet");
            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
            var jsonResponse = await response.Content.ReadFromJsonAsync<IEnumerable<TimeSheetDTO>>();
            jsonResponse.Should().NotBeNullOrEmpty();
            Assert.Equal(2, jsonResponse.Count());
            Assert.Equal("TSheet Test 1", jsonResponse.ElementAt(0).description);
            Assert.Equal("TSheet Test 2", jsonResponse.ElementAt(1).description);
        }

        [Fact, TestPriority(2)]
        public async void TimeSheetController_WhenPostValid_CreatesTimeSheet()
        {
            // Arrange
            var request = new TimeSheetClass
            {
                description = "TSheet Test 3",
                time = 8,
                overtime = 0,
                date = Convert.ToDateTime("03/05/2019"),
                clientID = 2,
                projectID = 2,
                categoryID = 2,
                memberID = 2
            };
            // Act
            var response = await _httpClient.PostAsJsonAsync("api/TimeSheet", request);
            var returned = await response.Content.ReadFromJsonAsync<TimeSheetClass>();
            // Assert
            Assert.NotNull(returned);
            Assert.Equal(request.description, returned.description);
        }

        [Fact, TestPriority(3)]
        public async void TimeSheetController_WhenIdValid_ReturnsTimeSheetDTO()
        {
            //Arrange
            int id = 1;
            //Act
            var response = await _httpClient.GetAsync(string.Format("api/TimeSheet/{0}", id));
            var returned = await response.Content.ReadFromJsonAsync<TimeSheetDTO>();
            //assert
            Assert.Equal(id, returned.sheetID);
        }

        [Fact, TestPriority(5)]
        public async void TimeSheetController_Put_ReturnsUpdatedTimeSheet()
        {
            //Arrange
            var request = new TimeSheetClass
            {
                sheetID = 1,
                description = "UPDATE TEST 1",
                time = 7,
                overtime = 0,
                date = Convert.ToDateTime("02/05/2019"),
                clientID = 1,
                projectID = 1,
                categoryID = 1,
                memberID = 1
            };
            //Act
            var response = await _httpClient.PutAsJsonAsync("api/TimeSheet", request);
            var returned = await response.Content.ReadFromJsonAsync<TimeSheetClass>();
            //Assert
            Assert.Equal(request.description, returned.description);
        }
        
        [Fact, TestPriority(6)]
        public async void TimeSheetController_Delete_WhenIdValid_GetReturnsBadRequest()
        {
            //arrange
            int id = 2;
            //act
            var deleteCheck = await _httpClient.DeleteAsync(string.Format("api/TimeSheet/{0}", id));
            var check = await _httpClient.GetAsync(string.Format("api/TimeSheet/{0}", id));
            //assert
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, check.StatusCode);
        }
    }
}
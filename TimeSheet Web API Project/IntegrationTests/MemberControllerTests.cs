using FluentAssertions;
using TimeSheet.Models;

namespace IntegrationTests
{
    [TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]
    public class MemberControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public MemberControllerTests(TestingWebAppFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact, TestPriority(1)]
        public async void MemberController_Get_Returns_MemberList()
        {
            // Act
            var response = await _httpClient.GetAsync("api/Member");
            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
            var jsonResponse = await response.Content.ReadFromJsonAsync<IEnumerable<Member>>();
            jsonResponse.Should().NotBeNullOrEmpty();
            Assert.Equal(2, jsonResponse.Count());
            Assert.Equal("Test Member 1", jsonResponse.ElementAt(0).memberName);
            Assert.Equal("Test Member 2", jsonResponse.ElementAt(1).memberName);
        }

        [Fact, TestPriority(2)]
        public async void MemberController_WhenPostValid_CreatesMember()
        {
            // Arrange
            var request = new Member
            {
                memberName = "New Member Test",
                hoursPerWeek = 40,
                status = 1,
                role = 1
            };
            // Act
            var response = await _httpClient.PostAsJsonAsync("api/Member", request);
            var returned = await response.Content.ReadFromJsonAsync<Member>();
            // Assert
            Assert.NotNull(returned);
            Assert.Equal(request.memberName, returned.memberName);
            Assert.Equal(request.hoursPerWeek, returned.hoursPerWeek);
            Assert.Equal(request.status, returned.status);
            Assert.Equal(request.role, returned.role);
        }

        [Fact, TestPriority(3)]
        public async void MemberController_WhenIdValid_ReturnsMember()
        {
            //arrange
            int id = 1;
            //act
            var response = await _httpClient.GetAsync(string.Format("api/Member/{0}", id));
            var returned = await response.Content.ReadFromJsonAsync<Member>();
            //assert
            Assert.Equal(id, returned.memberID);
        }

        [Fact, TestPriority(4)]
        public async void MemberContoller_Put_ReturnsUpdatedMember()
        {
            //arrange
            var request = new Member
            {
                memberID = 1,
                memberName = "TEST CHANGE",
            };
            //act

            var response = await _httpClient.PutAsJsonAsync("api/Member", request);
            var returned = await response.Content.ReadFromJsonAsync<Member>();
            //assert
            Assert.Equal(request.memberName, returned.memberName);
        }

        [Fact, TestPriority(5)]
        public async void MemberController_Delete_WhenIdValid_GetReturnsBadRequest()
        {
            //arrange
            int id = 2;
            //act
            var deleteCheck = await _httpClient.DeleteAsync(string.Format("api/Member/{0}", id));
            var check = await _httpClient.GetAsync(string.Format("api/Member/{0}", id));
            //assert

            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, check.StatusCode);
        }
    }
}
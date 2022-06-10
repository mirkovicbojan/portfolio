using FluentAssertions;
using TimeSheet.Models;

namespace IntegrationTests
{
    [TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]
    public class ProjectControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public ProjectControllerTests(TestingWebAppFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async void ProjectController_Get_Returns_ProjectList()
        {
            // Act
            var response = await _httpClient.GetAsync("api/Project");
            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
            var jsonResponse = await response.Content.ReadFromJsonAsync<IEnumerable<Project>>();
            jsonResponse.Should().NotBeNullOrEmpty();
            Assert.Equal(2, jsonResponse.Count());
            Assert.Equal("Test name 1", jsonResponse.ElementAt(0).projectName);
            Assert.Equal("Test name 2", jsonResponse.ElementAt(1).projectName);
        }

        [Fact]
        public async void ProjectController_WhenPostValid_CreatesProject()
        {
            //Arrange
            var request = new Project
            {
                projectName = "Test Project",
                projectDescription = "Test Project Description"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("api/Project", request);
            var returned = await response.Content.ReadFromJsonAsync<Project>();
            //Assert
            Assert.NotNull(returned);
            Assert.Equal(request.projectName, returned.projectName);
            Assert.Equal(request.projectDescription, returned.projectDescription);
        }

        [Fact]
        public async void ProjectController_WhenIdValid_ReturnsProject()
        {
            //arrange
            int id = 1;
            //act
            var response = await _httpClient.GetAsync(string.Format("api/Project/{0}", id));
            var returned = await response.Content.ReadFromJsonAsync<Project>();
            //assert
            Assert.Equal(id, returned.projectID);
        }

        [Fact]
        public async void ProjectController_Put_ReturnsUpdatedProject()
        {
            //arrange
            var request = new Project
            {
                projectID = 1,
                projectName = "TEST CHANGE",
            };
            //act
            var response = await _httpClient.PutAsJsonAsync("api/Project", request);
            var returned = await response.Content.ReadFromJsonAsync<Project>();
            //assert
            Assert.Equal(request.projectName, returned.projectName);
        }

        [Fact, TestPriority(5)]
        public async void ProjectController_Delete_WhenIdValid_GetReturnsBadRequest()
        {
            //arrange
            int id = 1;
            //act
            var deleteCheck = await _httpClient.DeleteAsync(string.Format("api/Project/{0}", id));
            var check = await _httpClient.GetAsync(string.Format("api/Project/{0}", id));
            //assert
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, check.StatusCode);
        }
    }
}
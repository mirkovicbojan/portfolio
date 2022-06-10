using FluentAssertions;
using TimeSheet.DTO_models;
using TimeSheet.Models;


namespace IntegrationTests
{
    [TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]
    public class ClientControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public ClientControllerTests(TestingWebAppFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact, TestPriority(1)]
        public async void ClientController_Get_Returns_ClientList()
        {
            //act
            var response = await _httpClient.GetAsync("api/Client");
            //assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
            var jsonResponse = await response.Content.ReadFromJsonAsync<IEnumerable<ClientDTO>>();
            jsonResponse.Should().NotBeNullOrEmpty();
            Assert.Equal(2, jsonResponse.Count());
            Assert.Equal("TestName", jsonResponse.ElementAt(0).clientName);
            Assert.Equal("TestName2", jsonResponse.ElementAt(1).clientName);
        }

        [Fact, TestPriority(2)]
        public async void ClientController_WhenPostValid_CreatesClient()
        {
            //Arrange
            var request = new Client
            {
                clientName = "Test Client",
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("api/Client", request);
            var returned = await response.Content.ReadFromJsonAsync<Client>();
            //Assert
            Assert.NotNull(returned);
            Assert.Equal(request.clientName, returned.clientName);
        }

        [Fact]
        public async void ClientController_WhenIdValid_ReturnsClient()
        {
            //arrange
            int id = 1;
            //act
            var response = await _httpClient.GetAsync(string.Format("api/Client/{0}", id));
            var returned = await response.Content.ReadFromJsonAsync<Client>();
            //assert
            Assert.Equal(id, returned.clientID);
        }

        [Fact, TestPriority(3)]
        public async void ClientController_Put_ReturnsUpdatedClient()
        {
            //arrange
            var request = new Client
            {
                clientID = 1,
                clientName = "TEST CHANGE",
            };
            //act
            var response = await _httpClient.PutAsJsonAsync("api/Client", request);
            var returned = await response.Content.ReadFromJsonAsync<Client>();
            //assert
            Assert.Equal(request.clientName, returned.clientName);
        }

        [Fact, TestPriority(5)]
        public async void ClientController_Delete_WhenValidId_CallsForDeletion()
        {
            //arrange
            int id = 2;
            //act
            await _httpClient.DeleteAsync(string.Format("api/Client/{0}", id));
            var check = await _httpClient.GetAsync(string.Format("api/Client/{0}", id));
            //assert
            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, check.StatusCode);
        }
    }
}
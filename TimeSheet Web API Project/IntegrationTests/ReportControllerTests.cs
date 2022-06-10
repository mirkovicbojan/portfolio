using TimeSheet.DTO_Models;
using FluentAssertions;

namespace IntegrationTests
{
    public class ReportControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        private readonly TestingWebAppFactory<Program> _factory;

        public ReportControllerTests(TestingWebAppFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = factory.CreateClient();
        }

        [Fact]
        public async void ReportController_SearchByProjectName_ReturnsReportList()
        {
            //Arrange
            var request = new ReportSearchDTO
            {
                projectName = "Test name 1"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("api/Report", request);
            var returned = await response.Content.ReadFromJsonAsync<IEnumerable<ReportDTO>>();
            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            returned.Should().NotBeNullOrEmpty();
            Assert.Equal(1, returned.Count());
            Assert.Equal(request.projectName, returned.First().projectName);
        }

        [Fact]
        public async void ReportController_SearchByMemberName_ReturnsReportList()
        {
            //Arrange
            var request = new ReportSearchDTO
            {
                memberName = "Test Member 1"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("api/Report", request);
            var returned = await response.Content.ReadFromJsonAsync<IEnumerable<ReportDTO>>();
            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            returned.Should().NotBeNullOrEmpty();
            Assert.Equal(1, returned.Count());
            Assert.Equal(request.memberName, returned.First().memberName);
        }

        [Fact]
        public async void ReportController_SearchByClientName_ReturnsReportList()
        {
            //Arrange
            var request = new ReportSearchDTO
            {
                clientName = "TestName"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("api/Report", request);
            var returned = await response.Content.ReadFromJsonAsync<IEnumerable<ReportDTO>>();
            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            returned.Should().NotBeNullOrEmpty();
            Assert.Equal(1, returned.Count());
            Assert.Equal(request.clientName, returned.First().clientName);
        }

        [Fact]
        public async void ReportController_SearchByCategoryName_ReturnsReportList()
        {
            //Arrange
            var request = new ReportSearchDTO
            {
                categoryName = "Back-End"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("api/Report", request);
            var returned = await response.Content.ReadFromJsonAsync<IEnumerable<ReportDTO>>();
            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            returned.Should().NotBeNullOrEmpty();
            Assert.Equal(1, returned.Count());
            Assert.Equal(request.categoryName, returned.First().categoryName);
        }

        [Fact]
        public async void ReportController_SearchByStartDate_ReturnsReportList()
        {
            //Arrange
            var request = new ReportSearchDTO
            {
                startDate = "01/04/2019"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("api/Report", request);
            var returned = await response.Content.ReadFromJsonAsync<IEnumerable<ReportDTO>>();
            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            returned.Should().NotBeNullOrEmpty();
            Assert.Equal(2, returned.Count());
            Assert.True(Convert.ToDateTime(request.endDate) < Convert.ToDateTime(returned.ElementAt(0).Date));
            Assert.True(Convert.ToDateTime(request.endDate) < Convert.ToDateTime(returned.ElementAt(1).Date));
        }

        [Fact]
        public async void ReportController_SearchByEndDate_ReturnsReportList()
        {
            //Arrange
            var request = new ReportSearchDTO
            {
                endDate = "01/04/2020"
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("api/Report", request);
            var returned = await response.Content.ReadFromJsonAsync<IEnumerable<ReportDTO>>();
            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            returned.Should().NotBeNullOrEmpty();
            Assert.Equal(2, returned.Count());
            Assert.True(Convert.ToDateTime(request.endDate) > Convert.ToDateTime(returned.ElementAt(0).Date));
            Assert.True(Convert.ToDateTime(request.endDate) > Convert.ToDateTime(returned.ElementAt(1).Date));
        }
        [Fact]
        public async void ReportController_generatePDF_ReturnsCorrectMediaType()
        {

            //Arrange
            var request = new ReportSearchDTO
            {
                memberName = "",
                clientName = "",
                projectName = "",
                categoryName = "",
                startDate = "",
                endDate = ""
            };
            //Act
            var response = await _factory.CreateClient().PostAsJsonAsync("api/Report/generatePDF", request);
            var content = await response.Content.ReadAsStringAsync();
            //Assert
            content.Should().NotBeNullOrEmpty();
            response.Content.Headers.ContentType.MediaType.Should().Be("application/pdf");
        }

        [Fact]
        public async void ReportController_writeToCsv_ReturnsCorrectMediaType()
        {
            //Arrange
            var request = new ReportSearchDTO
            {
                memberName = "",
                clientName = "",
                projectName = "",
                categoryName = "",
                startDate = "",
                endDate = ""
            };
            //Act
            var response = await _httpClient.PostAsJsonAsync("api/Report/writeToCSV", request);
            var responseByteArray = response.Content.ReadAsByteArrayAsync().Result;
            //Assert
            Assert.True(responseByteArray.Length > 0);
            response.Content.Headers.ContentType.MediaType.Should().Be("text/csv");
        }
    }
}
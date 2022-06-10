using FluentAssertions;
using TimeSheet.Models;

namespace IntegrationTests;

[TestCaseOrderer("IntegrationTests.PriorityOrderer", "IntegrationTests")]
public class CategoryControllerTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public CategoryControllerTests(TestingWebAppFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async void CategoryController_Get_Returns_Json()
    {
        //Act
        var response = await _httpClient.GetAsync("api/Category");
        //Assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        response.Content.Headers.ContentType.Should().NotBeNull();
        response.Content.Headers.ContentType.MediaType.Should().Be("application/json");
    }

    [Fact, TestPriority(1)]
    public async void CategoryController_Get_ReturnsCategoryList()
    {
        // act
        var response = await _httpClient.GetAsync("api/Category");
        var jsonResponse = await response.Content.ReadFromJsonAsync<IEnumerable<Category>>();
        // assert
        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        jsonResponse.Should().NotBeNullOrEmpty();
        Assert.Equal(2, jsonResponse.Count());
        Assert.Equal("Back-End", jsonResponse.ElementAt(0).categoryName);
        Assert.Equal("Front-End", jsonResponse.ElementAt(1).categoryName);
    }

    [Fact, TestPriority(2)]
    public async void CategoryController_WhenPostValid_CreatesCategory()
    {
        //Arrange
        var request = new Category
        {

            categoryName = "IntegrationTest Category Test",
        };
        //Act
        var response = await _httpClient.PostAsJsonAsync("api/Category", request);
        var returnedResponse = await response.Content.ReadFromJsonAsync<Category>();
        //Assert
        Assert.NotNull(returnedResponse);
        Assert.Equal(request.categoryName, returnedResponse.categoryName);
    }

    [Fact, TestPriority(3)]
    public async void CategoryController_GetWithId_ReturnsCategory()
    {
        //Arrange
        int id = 1;
        //Act
        var response = await _httpClient.GetAsync(string.Format("api/Category/{0}", id));
        var returnedResponse = await response.Content.ReadFromJsonAsync<Category>();
        //Assert
        Assert.Equal(id, returnedResponse.categoryID);
    }

    [Fact, TestPriority(4)]
    public async void CategoryController_Put_ReturnsUpdatedCategory()
    {
        //Arrange
        var request = new Category
        {
            categoryID = 1,
            categoryName = "IntegrationTest Category UPDATE",
            categoryDescription = "INTEGRATION TEST PUT UPDATE"
        };

        //Act
        var response = await _httpClient.PutAsJsonAsync("api/Category", request);
        var returned = await response.Content.ReadFromJsonAsync<Category>();
        //Assert
        Assert.Equal(request.categoryName, returned.categoryName);
        Assert.Equal(request.categoryDescription, returned.categoryDescription);
    }

    [Fact, TestPriority(5)]
    public async void CategoryController_Delete_WhenIdValid_CallsForDeletion()
    {
        //arrange
        int id = 2;
        //act
        await _httpClient.DeleteAsync(string.Format("api/Category/{0}", id));
        var check = await _httpClient.GetAsync(string.Format("api/Category/{0}", id));
        //assert

        Assert.Equal(System.Net.HttpStatusCode.InternalServerError, check.StatusCode);
    }
}
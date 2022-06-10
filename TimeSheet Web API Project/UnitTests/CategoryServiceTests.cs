using Moq;
using TimeSheet.CustomExceptions;
using TimeSheet.Models;
using TimeSheet.Repository.Interfaces;
using TimeSheet.Services;



namespace UnitTests;

public class CategoryServiceTests
{
    private readonly Mock<ICategoryRepository> repo = new Mock<ICategoryRepository>();
    private static ICategoryRepository createRepoForGetAll()
    {
        var stubRepository = new Mock<ICategoryRepository>();

        var categories = new List<Category>();

        Category backEnd = new Category
        {
            categoryID = 1,
            categoryName = "Back-End",
            categoryDescription = "Working on APIs"
        };
        Category frontEnd = new Category
        {
            categoryID = 2,
            categoryName = "Front-End",
            categoryDescription = "Working on GUI"
        };

        categories.Add(backEnd);
        categories.Add(frontEnd);

        stubRepository.Setup(c => c.GetAll()).Returns(categories);
        return stubRepository.Object;
    }


    [Fact]
    public void Get_WhenCalled_ReturnsAllCategories()
    {
        //Arrange
        CategoryService categoryService = new CategoryService(createRepoForGetAll());
        //Act
        IEnumerable<Category> categories = categoryService.GetAll();
        //Assert
        Assert.NotEmpty(categories);
    }

    [Fact]
    public void Get_ExistingId_ReturnsRightItem()
    {
        //Arrange
        var expectedCategory = new Category { categoryID = 5, categoryName = "Back end" };
        repo.Setup(c => c.GetById(5)).Returns(expectedCategory);
        CategoryService categoryService = new CategoryService(repo.Object);
        //Act
        var category = categoryService.GetOne(5);
        //Assert
        Assert.IsType<Category>(category);
        Assert.Equal(expectedCategory.categoryName, category.categoryName);
    }

    [Fact]
    public void Get_UnknownId_ReturnsKeyNotFoundException()
    {
        //Arrange
        repo.Setup(c => c.GetById(5)).Returns(new Category { categoryID = 5, categoryName = "Back end" });
        CategoryService categoryService = new CategoryService(repo.Object);
        //Act
        Action act = () => categoryService.GetOne(8);
        //Assert
        KeyNotFoundException exception = Assert.Throws<KeyNotFoundException>(act);
        Assert.Equal("Category with id: 8 not found.", exception.Message);
    }

    [Fact]
    public void Save_NewObject_ReturnsRightItem()
    {
        //Arrange
        Category category = new Category
        {
            categoryID = 1,
            categoryName = "NewCategory",
            categoryDescription = "Test New Category"
        };
        repo.Setup(c => c.Save(category)).Returns(category);
        CategoryService categoryService = new CategoryService(repo.Object);
        //Act
        var newCategory = categoryService.Save(category);
        //Assert
        repo.Verify(m => m.Save(category), Times.Once);
        Assert.IsType<Category>(newCategory);
        Assert.Equal("NewCategory", newCategory.categoryName);
    }

    [Fact]
    public void Save_InvalidObject_ReturnsInvalidObjectParamsException()
    {
        //Arrange
        Category category = new Category
        {
            categoryDescription = "Test New Category"
        };
        repo.Setup(c => c.Save(category)).Returns(category);
        CategoryService categoryService = new CategoryService(repo.Object);
        //Act
        Action act = () => categoryService.Save(category);
        //Assert
        InvalidObjectParamsException exception = Assert.Throws<InvalidObjectParamsException>(act);
        Assert.Equal("Category name cannot be empty.", exception.Message);
    }

    [Fact]
    public void UpdateOne_ObjectExists_ReturnsObject()
    {
        //Arrange
        Category updatedCategory = new Category
        {
            categoryID = 1,
            categoryName = "NewCategoryEDIT",
            categoryDescription = "Test New Category"
        };
        repo.Setup(c => c.Edit(updatedCategory)).Returns(updatedCategory);
        repo.Setup(c => c.GetById(1)).Returns(updatedCategory);
        CategoryService categoryService = new CategoryService(repo.Object);
        //Act
        var editedCategory = categoryService.UpdateOne(updatedCategory);
        //Assert
        repo.Verify(m => m.Edit(updatedCategory), Times.Once);
        Assert.IsType<Category>(editedCategory);
        Assert.Equal(updatedCategory.categoryName, editedCategory.categoryName);
    }

    [Fact]
    public void DeleteOne_ExistingObject_CallsRepository()
    {
        //Arrange
        Category forDeletion = new Category
        {
            categoryID = 1,
            categoryName = "NewCategoryEDIT",
            categoryDescription = "Test New Category"
        };
        repo.Setup(c => c.Delete(forDeletion));
        repo.Setup(c => c.GetById(1)).Returns(forDeletion);
        CategoryService categoryService = new CategoryService(repo.Object);
        //Act
        categoryService.DeleteOne(forDeletion.categoryID);
        //Assert
        repo.Verify(m => m.Delete(forDeletion), Times.Once);
    }
}
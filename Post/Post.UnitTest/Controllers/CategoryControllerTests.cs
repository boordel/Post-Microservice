using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using Post.Api.Controllers;
using Post.Application.RepositoryProxies.Contracts;
using Post.Domain.Entities.CategoryAggregate;
using Microsoft.AspNetCore.Mvc;

namespace Post.UnitTest.Controllers;
public class CategoryControllerTests
{
    private readonly Mock<ICategoryRepositoryProxy> _categoryRepository;

    public CategoryControllerTests()
    {
        _categoryRepository = new Mock<ICategoryRepositoryProxy>();
    }

    private static Task<IEnumerable<Category>?> GetCategoriesList()
    {
        IEnumerable<Category> list = new List<Category>()
        {
            new Category("Fake category")
        };
        return Task.FromResult(list)!;
    }

    [Fact]
    public void GetCategories_ShouldReturnList_WhenAnyCategoryExists()
    {
        // Arrange
        _categoryRepository
            .Setup(x => x.GetCategoriesAsync())
            .Returns(GetCategoriesList());
        CategoryController categoryController = new(_categoryRepository.Object);

        var expected = new OkObjectResult(null);
        var expectedListCount = 1;

        // Act
        var actual = categoryController.GetCategories();

        // Assert
        var resultObject = (OkObjectResult)actual.Result.Result!;
        var resultList = resultObject.Value as IEnumerable<Category>;

        Assert.Equal(expected.StatusCode, resultObject.StatusCode);
        Assert.Equal(expectedListCount, resultList!.Count());
    }

    [Fact]
    public void GetCategories_ShouldReturnNotFound_WhenCategoryNotExists()
    {
        // Arrange
        _categoryRepository
            .Setup(x => x.GetCategoriesAsync())
            .ReturnsAsync(() => null);
        CategoryController categoryController = new(_categoryRepository.Object);

        var expected = new NotFoundResult();

        // Act
        var actual = categoryController.GetCategories();

        // Assert
        Assert.Equal(expected.StatusCode, 
            ((NotFoundResult)actual.Result.Result!).StatusCode);
    }
}

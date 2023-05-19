using Moq;
using Post.Application.Contracts;
using Post.Application.Repositories;
using Post.Application.Settings;
using Post.Domain.Entities.CategoryAggregate;

namespace Post.UnitTest.Application.RepositoryProxies;
public class CategoryRepositoryProxyTests
{
    private readonly Mock<ICategoryRepository> _categoryRepository;
    private readonly Mock<ICacheService> _cacheService;

    public CategoryRepositoryProxyTests()
    {
        _categoryRepository= new Mock<ICategoryRepository>();
        _cacheService= new Mock<ICacheService>();
    }

    private static Task<IEnumerable<Category>?> GetCategoriesListFromDb()
    {
        IEnumerable<Category> list = new List<Category>()
        {
            new Category("Fake category from db")
        };
        return Task.FromResult(list)!;
    }

    private static Task<IEnumerable<Category>?> GetCategoriesListFromCache()
    {
        IEnumerable<Category> list = new List<Category>()
        {
            new Category("Fake category from cache")
        };
        return Task.FromResult(list)!;
    }

    [Fact]
    public void GetCategoriesAsync_ShouldReadFromDatabase_WhenNoCacheAvailable()
    {
        // Arrange
        var cacheKey = CacheSettings.Key_CategoryList;
        var timeSpan = TimeSpan.FromMinutes(CacheSettings.CacheValidationMinutes);
        var expected = GetCategoriesListFromDb();

        _categoryRepository
            .Setup(x => x.GetAllAsync())
            .Returns(GetCategoriesListFromDb());
        _cacheService
            .Setup(x => x.GetAsync<IEnumerable<Category>>(cacheKey))
            .ReturnsAsync(() => null);
        _cacheService
            .Setup(x => x.SetAsync(cacheKey, expected, timeSpan));

        CategoryRepositoryProxy categoryRepositoryProxy = new(_categoryRepository.Object, 
            _cacheService.Object);

        // Act
        var actual = categoryRepositoryProxy.GetCategoriesAsync();

        // Assert
        Assert.Equal(expected.Result!.First().Title, actual.Result!.First().Title);
    }

    [Fact]
    public async Task GetCategoriesAsync_ShouldReadFromCache_WhenCacheAvailable()
    {
        // Arrange
        var cacheKey = CacheSettings.Key_CategoryList;
        var timeSpan = TimeSpan.FromMinutes(CacheSettings.CacheValidationMinutes);
        var expected = GetCategoriesListFromCache();

        _categoryRepository
            .Setup(x => x.GetAllAsync())
            .Returns(GetCategoriesListFromDb());
        _cacheService
            .Setup(x => x.GetAsync<IEnumerable<Category>>(cacheKey))
            .ReturnsAsync(await GetCategoriesListFromCache());
        _cacheService
            .Setup(x => x.SetAsync(cacheKey, expected, timeSpan));

        CategoryRepositoryProxy categoryRepositoryProxy = new(_categoryRepository.Object,
            _cacheService.Object);

        // Act
        var actual = categoryRepositoryProxy.GetCategoriesAsync();

        // Assert
        Assert.Equal(expected.Result!.First().Title, actual.Result!.First().Title);
    }
}

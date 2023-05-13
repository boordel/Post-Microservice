using Post.Domain.Entities.CategoryAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Post.UnitTest.Domain.Aggregates;
public class CategoryAggregateTests
{
    [Fact]
    public void Create_ShouldCreateItem()
    {
        // Arrange
        var id = 1;
        var title = "category title";

        // Act
        var newCategory = new Category(id, title);

        // Assert
        Assert.NotNull(newCategory);
    }

    [Fact]
    public void Create_ShouldHaveValidArguments()
    {
        // Arrange
        var id = 1;
        var title = "";

        // Assert
        Assert.Throws<ArgumentNullException>("title", () => new Category(id, title));
    }
}

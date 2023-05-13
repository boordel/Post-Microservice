using Microsoft.Identity.Client;
using Post.Domain.Entities.PostAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Post.UnitTest.Domain.Aggregates;
public class PostAggregateTests
{
    [Fact]
    public void Create_ShouldCreateItem()
    {
        // Arrange
        var postId = 1;
        var title = "Fake post title";
        var headLine = "headline";
        var description = "description";
        var keywords = "some keywords";
        var categoryId = 1;

        // Act
        var newPost = new Post.Domain.Entities.PostAggregate.Post(
            postId, 
            title, 
            headLine, 
            description, 
            keywords, 
            categoryId);

        // Assert
        Assert.NotNull(newPost);
    }

    [Theory]
    [InlineData("title", "", "headLine")]
    [InlineData("headLine", "title", "")]
    public void Create_ShouldHaveValidArguments(string paramName, string title, string headLine)
    {
        // Arrange 
        var postId = 1;
        var description = "description";
        var keyword = "some keyword";
        var categoryId = 1;

        // Act

        // Assert
        Assert.Throws<ArgumentNullException>(paramName, () => new Post.Domain.Entities.PostAggregate.Post(
            postId,
            title,
            headLine,
            description,
            keyword,
            categoryId
            ));
    }

    [Fact]
    public void Create_ShouldHaveValidCategoryId()
    {
        // Arrange
        var postId = 1;
        var title = "Fake post title";
        var headLine = "headline";
        var description = "description";
        var keywords = "some keywords";
        var categoryId = -1;

        // Act

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>("categoryId", () => new Post.Domain.Entities.PostAggregate.Post(
            postId,
            title,
            headLine,
            description,
            keywords,
            categoryId));
    }

    [Fact]
    public void Create_ShouldAddMedia()
    {
        // Arrange
        var postId = 1;
        var title = "Fake post title";
        var headLine = "headline";
        var description = "description";
        var keywords = "some keywords";
        var categoryId = 1;
        var mediaTitle = "media title";
        var mediaType = PostTypes.Image;
        var mediaFile = "some url";

        var mediaCountExpected = 1;

        // Act
        var newPost = new Post.Domain.Entities.PostAggregate.Post(
            postId,
            title,
            headLine,
            description,
            keywords,
            categoryId);

        newPost.AddMedia(mediaTitle, mediaType, mediaFile);

        // Assert
        Assert.Equal(newPost.Medias.Count, mediaCountExpected);
    }

    [Theory]
    [InlineData("title", "", PostTypes.Image, "file")]
    [InlineData("type", "media title", PostTypes.None, "file")]
    [InlineData("mediaUrl", "media title", PostTypes.Video, "")]
    public void Create_ShouldMediaHaveValidArguments(string paramName, string mediaTitle, PostTypes mediaType, string mediaFile)
    {
        // Arrange
        var postId = 1;
        var title = "Fake post title";
        var headLine = "headline";
        var description = "description";
        var keywords = "some keywords";
        var categoryId = 1;

        // Act
        var newPost = new Post.Domain.Entities.PostAggregate.Post(
            postId,
            title,
            headLine,
            description,
            keywords,
            categoryId);

        // Assert
        Assert.Throws<ArgumentNullException>(paramName, () =>
            newPost.AddMedia(mediaTitle, mediaType, mediaFile));
    }
}

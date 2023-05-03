namespace Post.Api.Application.Queries.Category;

public class CategoryList
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int PostCount { get; set; } = 0;
}

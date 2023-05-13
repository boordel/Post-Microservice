using System.Text.Json.Serialization;

namespace Post.Domain.Entities.CategoryAggregate;
public class Category : Entity, IAggregateRoot
{
    public string Title { get; private set; }

    public Category(string title)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentNullException("title", "title must be specified");

        Title = title;
    }

    [JsonConstructorAttribute]
    public Category(int id, string title) :
        this(title)
    {
        SetId(id);
    }
}

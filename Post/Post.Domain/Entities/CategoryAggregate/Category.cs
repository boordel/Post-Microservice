namespace Post.Domain.Entities.CategoryAggregate;
public class Category : Entity, IAggregateRoot
{
    public string Title { get; private set; }

    public Category(string title)
    {
        Title = title;
    }
    public Category(int id, string title)
    {
        SetId(id);
        Title = title;
    }
}

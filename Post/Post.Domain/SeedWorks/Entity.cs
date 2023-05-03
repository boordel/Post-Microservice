namespace Post.Domain.SeedWorks;
public class Entity
{
    public int Id { get; private set; }

    public void SetId(int id)
    {
        Id = id;
    }
}

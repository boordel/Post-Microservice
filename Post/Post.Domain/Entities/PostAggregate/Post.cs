namespace Post.Domain.Entities.PostAggregate;
public class Post : Entity, IAggregateRoot
{
    public string Title { get; private set; }
    public string Headline { get; private set; }
    public string Description { get; private set; }
    public string Keywords { get; private set; }
    public int CategoryId { get; private set; }

    private readonly List<PostMedia> _medias;
    public IReadOnlyCollection<PostMedia> Medias => _medias;

    public Post(string title, string headline, string description, string keywords, int categoryId)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentNullException("title", "post title must be specified");
        if (string.IsNullOrEmpty(headline))
            throw new ArgumentNullException("headLine", "post headline must be specified");
        if (categoryId <= 0)
            throw new ArgumentOutOfRangeException("categoryId", "categoryId must be valid");


        Title = title;
        Headline = headline;
        Description = description;
        Keywords = keywords;
        CategoryId = categoryId;
        _medias = new();
    }
    public Post(int id, string title, string headline, string description, string keywords, int categoryId) :
        this(title, headline, description, keywords, categoryId)
    {
        SetId(id);
    }


    public void AddMedia(string title, PostTypes type, string mediaUrl)
    {
        var postMedai = new PostMedia(title, type, mediaUrl, Id);
        _medias.Add(postMedai);
    }
}

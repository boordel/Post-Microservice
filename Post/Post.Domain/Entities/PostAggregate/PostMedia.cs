namespace Post.Domain.Entities.PostAggregate;
public class PostMedia : Entity
{
    public string Title { get; private set; }
    public PostTypes Type { get; private set; }
    public string MediaUrl { get; private set; }
    public int PostId { get; private set; }

    public PostMedia(string title, PostTypes type, string mediaUrl, int postId)
    {
        Title = title;
        Type = type;
        MediaUrl = mediaUrl;
        PostId = postId;
    }
    public PostMedia(int id, string title, PostTypes type, string mediaUrl, int postId)
    {
        SetId(id);
        Title = title;
        Type = type;
        MediaUrl = mediaUrl;
        PostId = postId;
    }
}

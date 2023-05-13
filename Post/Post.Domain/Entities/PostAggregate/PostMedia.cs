namespace Post.Domain.Entities.PostAggregate;
public class PostMedia : Entity
{
    public string Title { get; private set; }
    public PostTypes Type { get; private set; }
    public string MediaUrl { get; private set; }
    public int PostId { get; private set; }

    public PostMedia(string title, PostTypes type, string mediaUrl, int postId)
    {
        if (string.IsNullOrEmpty(title))
            throw new ArgumentNullException("title", "media title must be specified");
        if (type == PostTypes.None)
            throw new ArgumentNullException("type", "media type must be specified");
        if (string.IsNullOrEmpty(mediaUrl))
            throw new ArgumentNullException("mediaUrl", "media file must be specified");

        Title = title;
        Type = type;
        MediaUrl = mediaUrl;
        PostId = postId;
    }
    public PostMedia(int id, string title, PostTypes type, string mediaUrl, int postId):
        this(title, type, mediaUrl, postId)
    {
        SetId(id);
    }
}

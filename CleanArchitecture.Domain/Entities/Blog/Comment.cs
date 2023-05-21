using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Generics;

namespace CleanArchitecture.Domain.Entities.Blog;

public sealed class Comment : EntityBase<CommentId>
{
    public Comment() { }
    private Comment(CommentId id, UserId userId, PostId postId, string content)
    {
        Id = id;
        UserId = userId;
        PostId = postId;
        Content = content;
    }

    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int Like { get; set; } = 0;
    public int Unlike { get; set; } = 0;
    public UserId? UserId { get; set; }
    public PostId PostId { get; set; }

    public static Comment Create(UserId userId, PostId postId, string content)
    {
        var comment = new Comment(new CommentId(Guid.NewGuid()), userId,postId, content);
        return comment;
    }
}

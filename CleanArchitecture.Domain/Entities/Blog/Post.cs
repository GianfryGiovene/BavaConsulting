using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Generics;

namespace CleanArchitecture.Domain.Entities.Blog;

public sealed class Post : EntityBase<PostId>
{
    private Post(UserId userId,string title, string content)
    {
        Title = title;
        Content = content;
        UserId = userId;
    }

    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int Like { get; set; } = 0;
    public int Unlike { get; set; } = 0;
    public UserId UserId { get; set; }
    public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();

    public static Post Create(UserId userId, string title, string content)
    {
        var post = new Post(userId, title, content);
        return post;
    }
}

using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Generics;

namespace CleanArchitecture.Domain.Entities.Blog;

public sealed class Post : EntityBase<PostId>
{
    public Post() { }
    private Post(PostId id,UserId userId,string title, string content, List<Category> categories)
    {
        Id = id;
        Title = title;
        Content = content;
        UserId = userId;
        Categories = categories;
    }

    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int Like { get; set; } = 0;
    public int Unlike { get; set; } = 0;
    public UserId UserId { get; set; }
    public List<Comment> Comments { get; set; } = new List<Comment>();
    public List<Category> Categories { get; set; } = new List<Category>();

    public static Post Create( UserId userId, string title, string content, List<Category> categories)
    {
        var post = new Post(new PostId(Guid.NewGuid()), userId, title, content, categories);
        return post;
    }

    public void CreateComment(UserId userId, string content)
    {
        var comment = Comment.Create(userId, this.Id, content);

        Comments.Add(comment);
    }
}

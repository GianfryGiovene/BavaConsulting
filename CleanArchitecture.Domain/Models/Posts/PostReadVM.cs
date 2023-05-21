using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Models.Comments;

namespace CleanArchitecture.Domain.Models.Posts;

public sealed class PostReadVM
{
    private PostReadVM(PostId id, UserId userId, string title, string content
        ,int like, int unlike, DateTime createdAt, IEnumerable<CommentReadVM> comments)
    {
        Id = id;
        UserId = userId;
        Title = title;
        Content = content;
        Like = like;
        Unlike = unlike;
        CreatedAt = createdAt;
        Comments = comments;
    }

    public PostId Id { get; set; }
    public UserId UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int Like { get; set; }
    public int Unlike { get; set; }
    public DateTime CreatedAt { get; set; }
    public IEnumerable<CommentReadVM> Comments { get; set; } = new List<CommentReadVM>();

    public static PostReadVM Create(PostId id, UserId userId, string title
        ,string content, int like, int unlike
        ,DateTime createdAt, IEnumerable<CommentReadVM> comments)
    {
        var postReadVm = new PostReadVM(id, userId, title, content, like, unlike, createdAt, comments);

        return postReadVm;
    }
}

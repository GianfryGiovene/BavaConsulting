using CleanArchitecture.Domain.Entities.Users;

namespace CleanArchitecture.Domain.Models.Comments;

public sealed class CommentReadVM
{
    private CommentReadVM(UserId userId, string content, int like, int unlike, DateTime createdAt)
    {
        UserId = userId;
        Content = content;
        Like = like;
        Unlike = unlike;
        CreatedAt = createdAt;
    }

    public UserId UserId { get; set; }
    public string Content { get; set; } = string.Empty;
    public int Like { get; set; }
    public int Unlike { get; set; }
    public DateTime CreatedAt { get; set; }

    public static CommentReadVM Create(UserId userId, string content, int like, int unlike, DateTime createdAt)
    {
        var commentRead = new CommentReadVM(userId,content,like, unlike, createdAt);

        return commentRead;
    }
}

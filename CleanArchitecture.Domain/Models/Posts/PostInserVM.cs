using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;

namespace CleanArchitecture.Domain.Models.Posts;

public sealed class PostInserVM
{
    public UserId UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public IEnumerable<CategoryId> Categories { get; set; } = new List<CategoryId>();
}

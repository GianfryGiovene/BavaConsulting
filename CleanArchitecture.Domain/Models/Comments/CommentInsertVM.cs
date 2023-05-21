using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;

namespace CleanArchitecture.Domain.Models.Comments;

public record CommentInsertVM(PostId PostId,UserId UserId, string Content);
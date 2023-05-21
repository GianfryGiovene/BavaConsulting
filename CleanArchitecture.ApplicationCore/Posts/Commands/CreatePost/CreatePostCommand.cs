using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Posts.Commands.CreatePost;

public record CreatePostCommand(UserId UserId
    ,string Title
    ,string Content
    ,IEnumerable<CategoryId> Categories) : IRequest<Post>;
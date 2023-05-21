using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Comments.Commands.CreateComment;

public record CreateCommentCommand(PostId PostId, UserId UserId, string Content) : IRequest<Comment>;

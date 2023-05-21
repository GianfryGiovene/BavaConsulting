using CleanArchitecture.ApplicationCore.Abstractions;
using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Comments.Commands.CreateComment;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateCommentCommand, Comment>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Comment> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request is null) throw new NullReferenceException();

            var comment = Comment.Create(request.UserId, request.PostId, request.Content);

            await _unitOfWork.CommentRepository.CreateAsync(comment);

            await _unitOfWork.CommitAsync();

            return comment;
        }
        catch
        {
            _unitOfWork.Dispose();
            throw new Exception();
        }
    }
}
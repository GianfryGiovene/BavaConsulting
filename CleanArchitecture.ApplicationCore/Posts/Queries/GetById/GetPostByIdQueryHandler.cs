using CleanArchitecture.ApplicationCore.Abstractions;
using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Models.Comments;
using CleanArchitecture.Domain.Models.Posts;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Posts.Queries.GetById;

public sealed class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, PostReadVM>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPostByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PostReadVM> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        List<CommentReadVM> comments = new List<CommentReadVM>();

        if (request.Id is null || request.Id.GetType() != typeof(PostId)) throw new Exception();

        var post = await _unitOfWork.PostRepository.GetAsync(u => u.Id == request.Id);

        if (post == null) throw new Exception();

        foreach(var comment in post.Comments)
        {
            var commentVm = CommentReadVM.Create(comment.UserId, comment.Content, comment.Like, comment.Unlike, comment.CreatedAt);

            comments.Add(commentVm);
        }

        var result = PostReadVM.Create(post.Id, post.UserId,post.Title,post.Content,post.Like, post.Unlike, post.CreatedAt, comments);

        return result;
    }
}

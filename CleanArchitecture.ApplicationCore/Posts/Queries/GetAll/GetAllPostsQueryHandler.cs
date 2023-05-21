using CleanArchitecture.ApplicationCore.Abstractions;
using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Models.Comments;
using CleanArchitecture.Domain.Models.Posts;
using CleanArchitecture.Domain.Models.User;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Posts.Queries.GetAll;

public sealed class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, IEnumerable<PostReadVM>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllPostsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PostReadVM>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Post> posts;

        if (request.Filter is null)
        {
            posts = await _unitOfWork.PostRepository.GetAllAsync();
        }
        else
        {
            posts = await _unitOfWork.PostRepository.GetAllAsync(u => u.Title.Contains(request.Filter.Trim()));
        }

        List<PostReadVM> postsRead = new List<PostReadVM>();
        foreach (var post in posts)
        {
            List<CommentReadVM> commentsVm = new List<CommentReadVM>();
            foreach(var comment in post.Comments)
            {
                var comntVM = CommentReadVM.Create(comment.UserId,comment.Content,comment.Like,comment.Unlike,comment.CreatedAt);
                commentsVm.Add(comntVM);
            }
            var postVm = PostReadVM.Create(post.Id,post.UserId,post.Title,post.Content,post.Like,post.Unlike,post.CreatedAt, commentsVm);
            postsRead.Add(postVm);
        }

        return postsRead;
    }
}

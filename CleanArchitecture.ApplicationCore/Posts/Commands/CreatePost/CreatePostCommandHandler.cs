using CleanArchitecture.ApplicationCore.Abstractions;
using CleanArchitecture.Domain.Entities.Blog;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Posts.Commands.CreatePost;

public sealed class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Post>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePostCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new NullReferenceException();

        List<Category> categories = new List<Category>();
        if(request.Categories is not null)
        {
            foreach (var catId in request.Categories) 
            {
                var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == catId, true);
                if(category is not null)
                {
                    categories.Add(category);
                }
            }
        }

        var post = Post.Create(request.UserId, request.Title,request.Content, categories);

        await _unitOfWork.PostRepository.CreateAsync(post);

        await _unitOfWork.CommitAsync();

        return post;
    }
}

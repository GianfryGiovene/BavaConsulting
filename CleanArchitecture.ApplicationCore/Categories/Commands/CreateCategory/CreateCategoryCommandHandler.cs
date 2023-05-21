using CleanArchitecture.ApplicationCore.Abstractions;
using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Categories.Commands.CreateCategory;

public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Category>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Category> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        if (request is null) throw new NullReferenceException();

        var category = Category.Create(request.name, request.description);

        await _unitOfWork.CategoryRepository.CreateAsync(category);

        await _unitOfWork.CommitAsync();        

        return category;
    }
}

using CleanArchitecture.ApplicationCore.Abstractions;
using CleanArchitecture.ApplicationCore.Categories.Queries.GetAllCategories;
using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Models.Category;
using CleanArchitecture.Domain.Models.User;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Categories.Queries.GetCategoryById;

public sealed class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryReadVM>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryReadVM> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id is null || request.Id.GetType() != typeof(CategoryId)) throw new Exception();

        var category = await _unitOfWork.CategoryRepository.GetAsync(u => u.Id == request.Id);

        if (category == null) throw new Exception();

        var result = new CategoryReadVM(category.Id, category.Name, category.Description);

        return result;
    }
}

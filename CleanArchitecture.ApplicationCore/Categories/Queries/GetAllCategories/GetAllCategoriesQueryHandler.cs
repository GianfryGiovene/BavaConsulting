using CleanArchitecture.ApplicationCore.Abstractions;
using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Models.Category;
using CleanArchitecture.Domain.Models.User;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Categories.Queries.GetAllCategories;

public sealed class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryReadVM>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<CategoryReadVM>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Category> categories;

        if (request.Filter is null)
        {
            categories = await _unitOfWork.CategoryRepository.GetAllAsync();
        }
        else
        {
            categories = await _unitOfWork.CategoryRepository.GetAllAsync(u => u.Name.Contains(request.Filter.Trim()));
        }

        List<CategoryReadVM> categoriesRead = new List<CategoryReadVM>();
        foreach (var category in categories)
        {
            var categoryVm = new CategoryReadVM(category.Id, category.Name,category.Description);
            categoriesRead.Add(categoryVm);
        }

        return categoriesRead;
    }
}

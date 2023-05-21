using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Models.Category;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Categories.Queries.GetAllCategories;

public record GetAllCategoriesQuery(string Filter) : IRequest<IEnumerable<CategoryReadVM>>;
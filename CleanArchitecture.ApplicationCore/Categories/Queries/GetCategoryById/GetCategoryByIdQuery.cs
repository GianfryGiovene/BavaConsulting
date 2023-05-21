using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Models.Category;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Categories.Queries.GetCategoryById;

public record GetCategoryByIdQuery(CategoryId Id) : IRequest<CategoryReadVM>;
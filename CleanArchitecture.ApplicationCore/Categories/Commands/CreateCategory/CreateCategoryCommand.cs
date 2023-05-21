using CleanArchitecture.Domain.Entities.Blog;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string name, string description) : IRequest<Category>;
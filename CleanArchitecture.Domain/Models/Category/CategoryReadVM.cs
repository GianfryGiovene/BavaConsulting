using CleanArchitecture.Domain.Entities.Blog;

namespace CleanArchitecture.Domain.Models.Category;

public record class CategoryReadVM(CategoryId CategoryId, string Name, string Description);

using CleanArchitecture.ApplicationCore.Abstractions.Repositories;
using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Infrastructure.Persistance;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}

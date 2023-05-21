using CleanArchitecture.ApplicationCore.Abstractions.Repositories;
using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Infrastructure.Persistance;

namespace CleanArchitecture.Infrastructure.Repositories;

internal class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(AppDbContext context) : base(context)
    {
    }
}

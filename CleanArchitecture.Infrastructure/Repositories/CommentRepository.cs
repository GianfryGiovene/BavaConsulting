using CleanArchitecture.ApplicationCore.Abstractions.Repositories;
using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Infrastructure.Persistance;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context)
    {
    }
}

using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.ApplicationCore.Abstractions;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Category> Categories { get; set; }

    public Task<int> SaveChangesAsync();

    public int SaveChanges();
}

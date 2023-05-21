using CleanArchitecture.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.ApplicationCore.Abstractions;

public interface IAppDbContext
{
    public DbSet<User> Users { get; set; }

    public Task<int> SaveChangesAsync();

    public int SaveChanges();
}

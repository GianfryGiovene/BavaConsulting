using CleanArchitecture.ApplicationCore.Abstractions.Repositories;
using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Infrastructure.Persistance;

namespace CleanArchitecture.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    internal readonly AppDbContext _context;
    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public Task Login(User user)
    {
        throw new NotImplementedException();
    }

    public void UpdateUser(User user)
    {
        if(user is null) throw new NullReferenceException(nameof(user));

        _context.Users.Update(user);
    }
}

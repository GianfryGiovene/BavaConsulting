using CleanArchitecture.Domain.Entities.Users;

namespace CleanArchitecture.ApplicationCore.Abstractions.Repositories;

public interface IUserRepository : IRepository<User>
{
    public void UpdateUser(User user);

    public Task Login(User user); 

}

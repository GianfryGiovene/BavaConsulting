using CleanArchitecture.ApplicationCore.Abstractions.Repositories;

namespace CleanArchitecture.ApplicationCore.Abstractions;

public interface IUnitOfWork : IDisposable
{
    public IUserRepository UserRepository { get; }

    Task CommitAsync();

    void Commit();
}

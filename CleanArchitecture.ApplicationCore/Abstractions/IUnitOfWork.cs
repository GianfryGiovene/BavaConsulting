using CleanArchitecture.ApplicationCore.Abstractions.Repositories;

namespace CleanArchitecture.ApplicationCore.Abstractions;

public interface IUnitOfWork : IDisposable
{
    public IUserRepository UserRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public IPostRepository PostRepository { get; }

    Task CommitAsync();

    void Commit();
}

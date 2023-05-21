using CleanArchitecture.ApplicationCore.Abstractions;
using CleanArchitecture.ApplicationCore.Abstractions.Repositories;
using CleanArchitecture.Infrastructure.Repositories;

namespace CleanArchitecture.Infrastructure.Persistance;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IUserRepository? _userRepository;
    private ICategoryRepository? _categoryRepository;
    private IPostRepository? _postRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
    public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context);
    public IPostRepository PostRepository => _postRepository ??= new PostRepository(_context);

    public void Commit()
    {
        _context.SaveChanges();
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}

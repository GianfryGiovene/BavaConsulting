namespace CleanArchitecture.Domain.Shared;

public sealed class Pagination<T> where T : class
{
    public List<T> PaginatedList { get; set; } = new List<T>();

    public int Pages { get; set; }

    public int CurrentPage { get; set; }
}

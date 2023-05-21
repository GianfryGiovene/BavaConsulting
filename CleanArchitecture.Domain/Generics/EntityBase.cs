namespace CleanArchitecture.Domain.Generics;

public abstract class EntityBase<T> where T : class
{
    public T Id { get; set; }
}

namespace CleanArchitecture.Domain.Shared;

//public class Result<T> : Result
//{
//    protected Result(bool isSuccess, Error error) : base(isSuccess, error)
//    {
//    }
//    private readonly T? _value;

//    protected internal Result(T? value, bool isSuccess, Error error) 
//    {
//        _value = value;
//    }

//    public T Value => IsSuccess
//        ? _value!
//        : throw new InvalidOperationException("The value of a failure result can not be accessed.");

//    public static implicit operator Result<T>(T? value) => Create(value);
//}

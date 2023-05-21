using System.Net;

namespace CleanArchitecture.Domain.Shared;

public class ApiResponse
{

    private ApiResponse(object? value,HttpStatusCode statusCode,bool isSuccess, List<string>? errorMessages )
    {
        Value = value;
        StatusCode = statusCode;
        IsSuccess = isSuccess;
        ErrorMessages = errorMessages;

    }

    public HttpStatusCode StatusCode { get; }
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public List<string> ErrorMessages { get; }
    public object Value { get; set; }


    public static ApiResponse Create(object value, HttpStatusCode statusCode, bool isSuccess, List<string>? errormessages)
    {
        var result = new ApiResponse(value,statusCode,isSuccess,errormessages);
        return result;
    }
}
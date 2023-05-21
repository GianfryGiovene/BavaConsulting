namespace CleanArchitecture.Domain.Shared;

public class Error
{
    protected Error(List<string> messages)
    { 
        Messages = messages;
    }

    public List<string> Messages { get; }

    public static Error Create(List<string> messages)
    {
        var error = new Error(messages);

        return error;
    }
}

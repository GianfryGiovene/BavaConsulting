namespace CleanArchitecture.Domain.Models.User;

public sealed record UserCreateVM(string Email
    ,string Password
    ,string FiscalCode
    ,string FirstName
    ,string LastName
    ,DateTime BirthDay);

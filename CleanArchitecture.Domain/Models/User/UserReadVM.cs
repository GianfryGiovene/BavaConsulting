using CleanArchitecture.Domain.Entities.Users;

namespace CleanArchitecture.Domain.Models.User;

public sealed class UserReadVM
{
    private UserReadVM(UserId userId
        ,string email
        ,string fiscalCode
        ,string firstName
        ,string lastName
        ,DateTime birthDay
        ,DateTime createdAd)
    {
        UserId = userId;
        Email = email;
        FiscalCode = fiscalCode;
        FirstName = firstName;
        LastName = lastName;
        BirthDay = birthDay;
        CreatedAd = createdAd;
    }

    public UserId UserId { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string FiscalCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDay { get; set; }
    public DateTime CreatedAd { get; init; }

    public static UserReadVM Create(UserId userId
        , string email
        , string fiscalCode
        , string firstName
        , string lastName
        , DateTime birthDay
        , DateTime createdAd)
    {
        var userRead = new UserReadVM(userId, email, fiscalCode, firstName, lastName, birthDay, createdAd);

        return userRead;
    }

}

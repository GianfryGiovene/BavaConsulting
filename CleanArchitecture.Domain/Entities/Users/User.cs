using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Domain.Generics;
using CleanArchitecture.Domain.Utilities;

namespace CleanArchitecture.Domain.Entities.Users;

public sealed class User : EntityBase<UserId>
{
    public User() { }
    private User(UserId userId,
        string email, 
        byte[] passwordHash, 
        byte[] passwordSalt,
        string fiscalCode,
        string firstName, 
        string lastName, 
        DateTime birthDay)
    {
        Id= userId;
        Email = email;
        FiscalCode = fiscalCode;
        FirstName = firstName;
        LastName = lastName;
        BirthDay = birthDay;
        PasswordHash = passwordHash;
        PasswordSalt = passwordSalt;
        CreatedAd = DateTime.UtcNow;
        Role = Role.USER;
    }

    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public string FiscalCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDay { get; set; }
    public DateTime CreatedAd { get; init; }
    public Role Role { get; set; }

    public static User Create(string email,
        string password,
        string fiscalCode,
        string firstName,
        string lastName,
        DateTime birthDay)
    {
        PswHashingUtil.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = new User(new UserId(Guid.NewGuid())
            ,email
            ,passwordHash
            ,passwordSalt
            ,fiscalCode
            ,firstName
            ,lastName
            ,birthDay);

        return user;
    }
}


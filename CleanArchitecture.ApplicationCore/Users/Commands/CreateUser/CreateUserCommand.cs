using CleanArchitecture.Domain.Entities.Users;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Users.Commands.CreateUser;

public sealed record CreateUserCommand(string Email
    ,string Password
    ,string FiscalCode
    ,string FirstName
    ,string LastName
    ,DateTime BirthDay) : IRequest<User>;

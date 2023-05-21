using CleanArchitecture.Domain.Entities.Users;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Users.Commands.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<string>;
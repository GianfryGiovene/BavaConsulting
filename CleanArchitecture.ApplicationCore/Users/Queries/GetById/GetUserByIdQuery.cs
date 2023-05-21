using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Models.User;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Users.Queries.GetById;

public sealed record GetUserByIdQuery(UserId Value) : IRequest<UserReadVM>;
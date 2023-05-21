using CleanArchitecture.Domain.Models.User;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Users.Queries.GetAll;

public sealed record GetAllUsersQuery(string? Filter) : IRequest<IEnumerable<UserReadVM>>;

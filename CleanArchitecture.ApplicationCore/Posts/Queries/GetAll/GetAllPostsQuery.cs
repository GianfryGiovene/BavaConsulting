using CleanArchitecture.Domain.Models.Posts;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Posts.Queries.GetAll;

public record GetAllPostsQuery(string? Filter) : IRequest<IEnumerable<PostReadVM>>;

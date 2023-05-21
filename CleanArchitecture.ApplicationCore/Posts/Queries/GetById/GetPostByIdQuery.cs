using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Models.Posts;
using MediatR;

namespace CleanArchitecture.ApplicationCore.Posts.Queries.GetById;

public record GetPostByIdQuery(PostId Id) : IRequest<PostReadVM>;
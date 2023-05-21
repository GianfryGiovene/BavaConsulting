using CleanArchitecture.ApplicationCore.Posts.Commands.CreatePost;
using CleanArchitecture.ApplicationCore.Posts.Queries.GetAll;
using CleanArchitecture.ApplicationCore.Posts.Queries.GetById;
using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Models.Posts;
using CleanArchitecture.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers;

[Route("api/[controller]"), Authorize]
[ApiController]
[EnableCors("MyAllowAllOrigins")]
public class PostController : ControllerBase
{
    private readonly ISender _sender;
    private ApiResponse _response;
    private HttpStatusCode _statusCode;

    public PostController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [EnableCors()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse>> CreateAsync(PostInserVM request)
    {
        try
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var post = await _sender.Send(new CreatePostCommand(request.UserId,request.Title,request.Content,request.Categories));

            if (post is null) throw new Exception();

            _statusCode = HttpStatusCode.Created;

            _response = ApiResponse.Create("Post Inserito.", _statusCode, true, null);

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _statusCode = HttpStatusCode.BadRequest;

            _response = ApiResponse.Create("Allerta.", _statusCode, false, new List<string> { ex.ToString() });
            return BadRequest(_response);
        }
    }

    [HttpGet("get-all")]
    [EnableCors()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse>> GetAllAsync(string? filter)
    {
        IEnumerable<PostReadVM> posts;
        try
        {

            posts = await _sender.Send(new GetAllPostsQuery(filter));
            return Ok(posts);

        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("{id}", Name = "get-post-by-id")]
    [EnableCors()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse>> GetByIdAsync(PostId id)
    {
        try
        {
            if (id is null || id.GetType() != typeof(UserId)) return BadRequest("Id non valido.");

            var post = await _sender.Send(new GetPostByIdQuery(id));

            return Ok(post);

        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }

    }
}

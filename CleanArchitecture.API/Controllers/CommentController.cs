using CleanArchitecture.ApplicationCore.Comments.Commands.CreateComment;
using CleanArchitecture.Domain.Models.Comments;
using CleanArchitecture.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ISender _sender;
    private ApiResponse _response;
    private HttpStatusCode _statusCode;

    public CommentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> CreateAsync(CommentInsertVM request)
    {
        try
        {
            if (request is null) throw new ArgumentNullException(nameof(request));

            var comment = await _sender.Send(new CreateCommentCommand(request.PostId,request.UserId, request.Content));

            if (comment is null)
            {
                _statusCode = HttpStatusCode.BadRequest;

                return BadRequest("Qualcosa è andato storto.");
            }
            _statusCode = HttpStatusCode.Created;

            _response = ApiResponse.Create("Utente registrato", _statusCode, true, null);

            return CreatedAtRoute("get-comment-by-id", new { id = comment.Id }, comment);
        }
        catch (Exception ex)
        {
            _statusCode = HttpStatusCode.BadRequest;

            _response = ApiResponse.Create("Allerta.", _statusCode, false, new List<string> { ex.ToString() });
            return BadRequest(_response);
        }
    }
}

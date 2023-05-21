using CleanArchitecture.ApplicationCore.Users.Commands.CreateUser;
using CleanArchitecture.ApplicationCore.Users.Commands.Login;
using CleanArchitecture.ApplicationCore.Users.Queries.GetAll;
using CleanArchitecture.ApplicationCore.Users.Queries.GetById;
using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Models.User;
using CleanArchitecture.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[EnableCors("MyAllowAllOrigins")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;
    private HttpStatusCode statusCode;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> CreateUserAsync([FromBody]UserCreateVM request)
    {
        ApiResponse response;
        try
        {
            
            if (request is null) throw new ArgumentNullException(nameof(request));

            var user = await _sender.Send(new CreateUserCommand(
                request.Email
                ,request.Password
                ,request.FiscalCode
                ,request.FirstName
                ,request.LastName
                ,request.BirthDay));

            if(user is null)
            {
                statusCode = HttpStatusCode.BadRequest;
                
                return BadRequest("Qualcosa è andato storto.");
            }

            statusCode = HttpStatusCode.Created;

            response = ApiResponse.Create("Utente registrato", statusCode, true, null);


            return CreatedAtRoute("GetUserById", new { id = user.Id }, user);


        }
        catch(Exception ex) 
        {
            
            statusCode = HttpStatusCode.BadRequest;

            response = ApiResponse.Create("Allerta.", statusCode, false, new List<string> { ex.ToString() });
            return BadRequest(response);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login([FromBody] LoginVM request)
    {
        try
        {
            string token = await _sender.Send(new LoginCommand(
                request.Email,
                request.Password));

            return Ok(token);

        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("getall")]
    [EnableCors()]
    public async Task<ActionResult<IEnumerable<UserReadVM>>> GetAllAsync(string? filter)
    {
        IEnumerable<UserReadVM> users; 
        try
        {
            
            users = await _sender.Send(new GetAllUsersQuery(filter));
            return Ok(users);
            
        }catch(Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("{id}", Name = "GetUserById")]
    public async Task<ActionResult<UserReadVM>> GetUserById([FromQuery]UserId id)
    {
        try
        {
            if (id is null || id.GetType() != typeof(UserId)) return BadRequest("Id non valido.");

            var user = await _sender.Send(new GetUserByIdQuery(id));

            return Ok(user);

        }catch(Exception ex)
        {
            return BadRequest(ex);
        }
    }
}

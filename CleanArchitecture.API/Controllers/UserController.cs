using CleanArchitecture.ApplicationCore.Users.Commands.CreateUser;
using CleanArchitecture.ApplicationCore.Users.Commands.Login;
using CleanArchitecture.ApplicationCore.Users.Queries.GetAll;
using CleanArchitecture.ApplicationCore.Users.Queries.GetById;
using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Models.User;
using CleanArchitecture.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    private ApiResponse _response;
    private HttpStatusCode _statusCode;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [EnableCors()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse>> CreateUserAsync([FromBody]UserCreateVM request)
    {
        
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

            if (user is null) throw new Exception("Qualcosa è andato storto.");

            _statusCode = HttpStatusCode.Created;

            _response = ApiResponse.Create("Utente registrato", _statusCode, true, null);

            return CreatedAtRoute("get-user-by-id", new { id = user.Id }, user);

        }catch(EntityAlreadyExistsException ex)
        {
            _statusCode = HttpStatusCode.BadRequest;
            _response = ApiResponse.Create(null, _statusCode, false, new List<string> { ex.ToString() });
            return BadRequest(_response);
        }
        catch(Exception ex) 
        {
            _statusCode = HttpStatusCode.BadRequest;
            _response = ApiResponse.Create(null, _statusCode, false, new List<string> { ex.ToString() });
            return BadRequest(_response);
        }
    }

    [HttpPost("login")]
    [EnableCors()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Login([FromBody] LoginVM request)
    {
        try
        {
            string token = await _sender.Send(new LoginCommand(
                request.Email,
                request.Password));
            
            _statusCode= HttpStatusCode.OK;
            _response = ApiResponse.Create(token, _statusCode, true, null);

            return Ok(_response);

        }
        catch (Exception ex)
        {
            _statusCode = HttpStatusCode.BadRequest;
            _response = ApiResponse.Create(null, _statusCode, false, new List<string> { ex.ToString() });
            return BadRequest(_response);
        }
    }

    [HttpGet("getall"), Authorize(Roles = "ADMIN")]
    [EnableCors()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UserReadVM>>> GetAllAsync(string? filter)
    {
        IEnumerable<UserReadVM> users; 
        try
        {
            
            users = await _sender.Send(new GetAllUsersQuery(filter));

            _statusCode= HttpStatusCode.OK;
            _response = ApiResponse.Create(users, _statusCode, true, null);
            return Ok(_response);
            
        }catch(Exception ex)
        {
            _statusCode = HttpStatusCode.BadRequest;
            _response = ApiResponse.Create(null, _statusCode, false, new List<string> { ex.ToString() });
            return BadRequest(ex);
        }
    }

    [HttpGet("{id}", Name = "get-by-id"), Authorize]
    [EnableCors()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse>> GetUserById([FromQuery]UserId id)
    {
        try
        {
            if (id is null || id.GetType() != typeof(UserId)) throw new InvalidIdException("Id non valido.");

            var user = await _sender.Send(new GetUserByIdQuery(id));

            if (user is null) throw new EntityNotFoundException("Utente non trovato");

            return Ok(user);

        }catch(InvalidIdException ex)
        {
            _statusCode = HttpStatusCode.NotFound;
            _response = ApiResponse.Create(null, _statusCode, false, new List<string> { ex.ToString() });
            return BadRequest(_response);
        }
        catch(EntityNotFoundException ex)
        {
            _statusCode = HttpStatusCode.NotFound;
            _response = ApiResponse.Create(null, _statusCode, false, new List<string> { ex.ToString() });
            return NotFound(_response);

        }
        catch(Exception ex)
        {
            _statusCode = HttpStatusCode.NotFound;
            _response = ApiResponse.Create(null, _statusCode, false, new List<string> { ex.ToString() });
            return BadRequest(_response);
        }
    }
}

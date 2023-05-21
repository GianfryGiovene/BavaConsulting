using Azure;
using CleanArchitecture.ApplicationCore.Categories.Commands.CreateCategory;
using CleanArchitecture.ApplicationCore.Categories.Queries.GetAllCategories;
using CleanArchitecture.ApplicationCore.Categories.Queries.GetCategoryById;
using CleanArchitecture.ApplicationCore.Users.Queries.GetAll;
using CleanArchitecture.ApplicationCore.Users.Queries.GetById;
using CleanArchitecture.Domain.Entities.Blog;
using CleanArchitecture.Domain.Entities.Users;
using CleanArchitecture.Domain.Models.Category;
using CleanArchitecture.Domain.Models.User;
using CleanArchitecture.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ISender _sender;
    private ApiResponse _response;
    private HttpStatusCode _statusCode;

    public CategoryController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse>> CreateCategoryAsync(CategoryInsertVM request)
    {
        try
        {

            if (request is null) throw new ArgumentNullException(nameof(request));

            var category = await _sender.Send(new CreateCategoryCommand(request.name,request.description));

            if (category is null) throw new Exception();

            _statusCode = HttpStatusCode.Created;

            _response = ApiResponse.Create("Categoria Inserita.", _statusCode, true, null);


            return CreatedAtRoute("get-by-id", new { id = category.Id }, _response);
        }
        catch(Exception ex)
        {
            _statusCode = HttpStatusCode.BadRequest;

            _response = ApiResponse.Create("Allerta.", _statusCode, false, new List<string> { ex.ToString() });
            return BadRequest(_response);
        }
    }

    [HttpGet("get-all")]
    public async Task<ActionResult<ApiResponse>> GetAllAsync(string? filter)
    {
        IEnumerable<CategoryReadVM> categories;
        try
        {
            categories = await _sender.Send(new GetAllCategoriesQuery(filter));
            _statusCode = HttpStatusCode.OK;
            _response = ApiResponse.Create(categories, _statusCode, true, null);
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _statusCode = HttpStatusCode.BadRequest;
            _response = ApiResponse.Create(null, _statusCode, false, new List<string> { ex.ToString() });
            return BadRequest(_response);
        }
    }

    [HttpGet("{id}", Name = "get-category-by-id")]
    public async Task<ActionResult<ApiResponse>> GetById(CategoryId id)
    {
        try
        {
            if (id is null || id.GetType() != typeof(UserId)) throw new Exception();

            var category = await _sender.Send(new GetCategoryByIdQuery(id));
            if(category is null) throw new Exception();

            _statusCode= HttpStatusCode.OK;
            _response = ApiResponse.Create(category, _statusCode, true, null);

            return Ok(_response);
        }
        catch(Exception ex)
        {
            _statusCode = HttpStatusCode.BadRequest;
            _response = ApiResponse.Create(null, _statusCode, false, new List<string> { ex.ToString() });
            return BadRequest(_response);
        }
    }
}

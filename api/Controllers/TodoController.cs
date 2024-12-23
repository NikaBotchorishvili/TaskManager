using System.Linq.Expressions;
using api.Config;
using api.Dtos.TodoItem;
using api.Interfaces;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace api.Controllers;

[Route("api/tasks/")]
[ApiController]
public class TodoController: ControllerBase
{
    private readonly DatabaseContext _context ;
    private readonly IRepository<TodoItem, CreateTodoDto, UpdateTodoDto> _repo;
    private readonly IUserService _userService;
    public TodoController(IRepository<TodoItem, CreateTodoDto, UpdateTodoDto> repo, DatabaseContext context, IUserService userService)
    {
        _context = context;
        _repo = repo;
        _userService = userService;
    }

    [HttpGet("all/")]
    [SwaggerOperation(
        Summary = "Get All the tasks. filters between dates",
        Description = "Retrieves all of the tasks in the database"
    )]
    [Authorize()]
    public async Task<IActionResult> GetAll([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        startDate ??= DateTime.MinValue;
        endDate ??= DateTime.MaxValue;
        
        Expression<Func<TodoItem, bool>> filter = (TodoItem item) => item.CreatedAt > startDate && item.CreatedAt < endDate;
        
        return Ok(await _repo.GetAllAsync(filter));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Get All the tasks. filters between dates",
        Description = "Retrieves all of the tasks in the database"
    )]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var item = await _repo.GetAsync(id);
        
        if (item == null)
        {
            return NotFound();
        }
        
        return Ok(item);
    }

    [HttpPost("")]
    [Authorize]
    public async Task<IActionResult> Post([FromBody] CreateTodoDto createDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = _userService.GetUserIdFromClaims();

        if (userId == null)
        {
            return Unauthorized();
        }
        try
        {
            var model = await _repo.CreateAsync(createDto, userId);
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving TodoItem: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTodoDto updateDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var item = await _repo.UpdateEntity(id, updateDto);
            if(item == null) return NotFound();
            return Ok(item);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            await _repo.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
        return NoContent();
    }
}
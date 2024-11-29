using api.Config;
using api.Dtos.TodoItem;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace api.Controllers;

[Route("api/tasks/")]
[ApiController]
public class TodoController: ControllerBase
{
    private readonly DatabaseContext _context ;
    public TodoController(DatabaseContext databaseContext)
    {
        _context = databaseContext;
    }

    [HttpGet("all/")]
    [SwaggerOperation(
        Summary = "Get All the tasks. filters between dates",
        Description = "Retrieves all of the tasks in the database"
    )]
    public IActionResult GetAll([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
    {
        startDate ??= DateTime.MinValue;
        endDate ??= DateTime.MaxValue;
        var items = _context.TodoItems
            .Where(item => item.CreatedAt > startDate && item.CreatedAt < endDate )
            .ToList()
            .Select(item => item.ToTodoDto());
        return Ok(items);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get All the tasks. filters between dates",
        Description = "Retrieves all of the tasks in the database"
    )]
    public IActionResult GetById([FromRoute] int id)
    {

        var item = _context.TodoItems.SingleOrDefault(ite => ite.Id == id);


        if (item == null)
        {
            return NotFound();
        }
        
        return Ok(item.ToTodoDto());
    }

    [HttpPost("")]
    public async Task<IActionResult> Post([FromBody] CreateTodoDto createDto)
    {
        var model = createDto.ToTodoFromCreateDto();
   
        try
        {
            _context.TodoItems.Add(model);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving TodoItem: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
        var todoDto = model.ToTodoDto();

        return CreatedAtAction(nameof(GetById), new { id = model.Id }, todoDto);
    }
}
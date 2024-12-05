using api.Dtos.TodoItem;
using api.Models;

namespace api.Mappers;

public static class TodoMapper
{
    public static TodoDto ToTodoDto(this TodoItem todoModel)
    {
        var todoDto = new TodoDto
        {
            Id = todoModel.Id,
            Title = todoModel.Title,
            Description = todoModel.Description,
            CreatedAt = todoModel.CreatedAt,
            UpdateAt = todoModel.UpdateAt
        };

       
        return todoDto;
    }

    public static TodoItem ToTodoFromCreateDto(this CreateTodoDto createDto, string userId)
    {   
        Console.WriteLine(createDto.DueDate);
        var createdTodoDto = new TodoItem
        {
            Title = createDto.Title,
            Description = createDto.Description,
            DueDate = createDto.DueDate,
            Completed =  createDto.Completed,
            UserId = userId
        };
        Console.WriteLine(createdTodoDto.DueDate);
        return createdTodoDto;
    }
}
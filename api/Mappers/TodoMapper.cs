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

    public static TodoItem ToTodoFromCreateDto(this CreateTodoDto createDto)
    {
        var createTodoDto = new TodoItem
        {
            Title = createDto.Title,
            Description = createDto.Description,
            DueDate = createDto.DueDate,
        };

        return createTodoDto;
    }
}
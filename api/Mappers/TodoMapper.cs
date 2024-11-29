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
            CreatedAt = todoModel.CreatedAt,
            UpdateAt = todoModel.UpdateAt
        };

       
        return todoDto;
    }
}

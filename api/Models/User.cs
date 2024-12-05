using Microsoft.AspNetCore.Identity;

namespace api.Models;

public class User: IdentityUser
{
    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();

}
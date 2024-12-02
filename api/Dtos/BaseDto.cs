using System.ComponentModel.DataAnnotations;

namespace api.Dtos;

public class BaseDto
{
    public int Id { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime CreatedAt { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime UpdateAt { get; set; }
}

using System.Linq.Expressions;
using api.Interfaces;
using api.Config;
using api.Dtos.TodoItem;
using api.Mappers;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories;

public class TodoRepo: IRepository<TodoItem, CreateTodoDto, UpdateTodoDto>
{
    private readonly DbSet<TodoItem> _dbSet;
    private readonly DatabaseContext _context; 

    public TodoRepo(DatabaseContext dbContext)
    {
        _dbSet = dbContext.Set<TodoItem>();
        _context = dbContext;
     
    }
    public async Task<IEnumerable<TodoItem>> GetAllAsync(Expression<Func<TodoItem, bool>> filter)
    {

        return await _dbSet.Where(filter).ToListAsync();
    }

    public async Task<TodoItem?> GetAsync(int id)
    {
        var item = await _dbSet.SingleOrDefaultAsync((item) => item.Id == id);
        return item;
    }

    public async Task<TodoItem> CreateAsync(CreateTodoDto createTodo)
    {
        var model = createTodo.ToTodoFromCreateDto();

        try
        {
            _dbSet.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new ApplicationException("Failed to create TodoItem.", ex);
        }
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.TodoItems.FirstOrDefaultAsync(ite => ite.Id == id);

        if (item == null)
        {
            throw new Exception("Item is not present");
        }

        try
        {
            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw new Exception("Item is not present", ex);
        }
    }

    public async Task<TodoItem> UpdateEntity(int id, UpdateTodoDto entity)
    {
        var item = await this.GetAsync(id);
        if (item == null)
        {
            throw new ApplicationException("Item not found");
        }
        
        item.Title = entity.Title;
        item.Description = entity.Description;
        item.Completed = entity.Completed;
        item.DueDate = entity.DueDate;

        try
        {
            await _context.SaveChangesAsync();
            return item;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
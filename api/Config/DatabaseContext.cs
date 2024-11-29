using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.Config;

public class DatabaseContext: DbContext
{
    public DatabaseContext(DbContextOptions dbContextOptions): base(dbContextOptions)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var entityTypeClrType = entityType.ClrType;

            var createdAtProperty = entityTypeClrType.GetProperty("CreatedAt");
            var updateAtProperty = entityTypeClrType.GetProperty("UpdateAt");

            if (createdAtProperty != null && createdAtProperty.PropertyType == typeof(DateTime))
            {
                modelBuilder.Entity(entityTypeClrType)
                    .Property("CreatedAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP"); 
            }

            if (updateAtProperty != null && updateAtProperty.PropertyType == typeof(DateTime))
            {
                modelBuilder.Entity(entityTypeClrType)
                    .Property("UpdateAt")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            }
        }

        base.OnModelCreating(modelBuilder);
    }

}
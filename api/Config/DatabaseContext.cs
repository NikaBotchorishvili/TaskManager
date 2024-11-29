using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(), 
                v => v.ToUniversalTime()   
            );

            // Loop through each property in the entity
            foreach (var property in entityTypeClrType.GetProperties())
            {
                
                if (property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
                {
                    modelBuilder.Entity(entityTypeClrType)
                        .Property(property.Name)
                        .HasConversion(dateTimeConverter);
                }
            }

        }

        base.OnModelCreating(modelBuilder);
    }

}
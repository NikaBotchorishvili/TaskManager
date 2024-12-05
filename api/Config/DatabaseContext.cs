using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace api.Config;

public class DatabaseContext: IdentityDbContext<User>
{
    public DatabaseContext(DbContextOptions dbContextOptions): base(dbContextOptions)
    {
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {

            var entityTypeClrType = entityType.ClrType;

            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(), 
                v => v.ToUniversalTime()   
            );
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
        List<IdentityRole> identityRoles =
        [
            new()
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
            },

            new()
            {
                Name = "User",
                NormalizedName = "USER",
            }

        ];
     
        modelBuilder.Entity<IdentityRole>().HasData(identityRoles);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.ConfigureWarnings(warnings => 
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning)
        );
    }
}
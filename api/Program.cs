using System.Text.Json.Serialization;
using api.Config;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;


Env.Load();

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Task API",
        Version = "0.1V"
    });
    options.EnableAnnotations();
});
var connectionString = builder.Configuration["DATABASE_URL"];
Console.WriteLine($"Connection string: {connectionString}");    
if (string.IsNullOrEmpty(connectionString))
{
    throw new ArgumentNullException("DATABASE_URL", "Connection string is missing");
}

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(connectionString);
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
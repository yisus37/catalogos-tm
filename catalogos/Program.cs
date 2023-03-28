using dbModel.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var dbHost = Environment.GetEnvironmentVariable("DB_HOST")??"localhost:5432";
var dbUser = Environment.GetEnvironmentVariable("DB_USER")??"a";
var dbDatabase = Environment.GetEnvironmentVariable("DB_NAME")??"postgres";
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD")??"password"; 
var port = Environment.GetEnvironmentVariable("APP_PORT")??"8009";

/* var dbHost ="localhost:5432";
var dbUser = "a";
var dbDatabase = "postgres";
var dbPassword = "password";
var port = "8005"; */
var connectionString = $"Host={dbHost}; Database={dbDatabase}; Username={dbUser}; Password={dbPassword}";
builder.Services.AddDbContext<PostgresContext>(
    options =>
    {
        options.UseNpgsql(connectionString);
    }
);
builder.WebHost.UseUrls($"http://*:{port}"); 
builder.Services.AddScoped<PostgresContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

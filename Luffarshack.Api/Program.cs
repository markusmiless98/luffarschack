using Luffarschack.Core.Interfaces;
using Luffarschack.Core.Services;
using Luffarschack.Database;
using Luffarschack.Orchestration;
using Luffarschack.Orchestration.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5090/api/Piece","http://localhost:5090/api/game", "http://localhost:5090")
              .AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

builder.Services.AddOpenApi();

builder.Services.AddControllers();

var dbPath = Path.Combine(builder.Environment.ContentRootPath, "Luffarschack.db");

builder.Services.AddSingleton(new SqliteDatabase(dbPath));
builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddScoped<IMoveValidator, MoveValidator>();
builder.Services.AddScoped<IVictoryStatusValidator, VictoryStatusValidator>();
builder.Services.AddScoped<IGameBuilder, GameBuilder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting(); 
app.UseCors("AllowFrontend"); // must come before UseAuthorization/MapControllers

app.UseAuthorization();
app.MapControllers();

app.Run();

// Top-level statements make the compiler generate a hidden, internal Program class.
// Declaring it here as public partial merges with that class and makes it visible,
// so the test project can use WebApplicationFactory<Program>.
public partial class Program { }
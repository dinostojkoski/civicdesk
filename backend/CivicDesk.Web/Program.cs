using CivicDesk.Repository.Context;
using CivicDesk.Repository.Implementation;
using CivicDesk.Repository.Interface;
using CivicDesk.Service.Implementation;
using CivicDesk.Service.Interface;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? 
                       throw new InvalidOperationException("Connection string 'DefaultConnection' was not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(connectionString));

builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Repositories
builder.Services.AddScoped<ICityRepository, CityRepository>();

// Services
builder.Services.AddScoped<ICityService, CityService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
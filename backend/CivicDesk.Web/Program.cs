using CivicDesk.Repository.Context;
using CivicDesk.Repository.Implementation;
using CivicDesk.Repository.Interface;
using CivicDesk.Repository.Seed;
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
builder.Services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();

// Services
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IMunicipalityService, MunicipalityService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    await LocationSeeder.SeedAsync(context);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
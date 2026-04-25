using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddDbContext<StoreContext>( opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    // opt.UseSqlServer(
    // sqlServerOptionsAction: sqlOptions =>
    // {
    //     sqlOptions.EnableRetryOnFailure(
    //         maxRetryCount: 10,
    //         maxRetryDelay: TimeSpan.FromSeconds(30),
    //         errorNumbersToAdd: null);
    // });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

// app.UseAuthorization();
// app.UseAuthorization();

app.MapControllers();

app.Run();

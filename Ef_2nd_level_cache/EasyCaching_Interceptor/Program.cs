using EasyCaching_Interceptor.Persistence;
using EasyCaching_Interceptor.SeedData;
using EasyCaching_Interceptor.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("2ndLevelCache"));

builder.Services.AddScoped<UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.Services.CreateScope().ServiceProvider.SeedUsersAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

await app.RunAsync();
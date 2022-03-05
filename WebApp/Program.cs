using Data;
using Microsoft.EntityFrameworkCore;
using WebApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProductConnection")));
var app = builder.Build();

app.UseMiddleware<TestMiddleware>();
SeedData.SeedDatabase(app);

app.Run();

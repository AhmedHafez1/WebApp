using Data;
using Microsoft.EntityFrameworkCore;
using WebApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProductConnection")));
builder.Services.AddLogging();
builder.Services.AddControllers();

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseMiddleware<TestMiddleware>();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async context => await context.Response.WriteAsync("Welcome"));
    //endpoints.MapWebService();
    endpoints.MapControllers();
});
SeedData.SeedDatabase(app);

app.Run();

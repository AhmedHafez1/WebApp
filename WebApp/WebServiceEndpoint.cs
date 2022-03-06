using Data;
using Models;
using System.Text.Json;

namespace WebApp
{
    public static class WebServiceEndpoint
    {
        private static string BASEURL = "api/products";
        public static void MapWebService(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapGet($"{BASEURL}/{{id}}", async (context) =>
            {
                long key = long.Parse(context?.Request.RouteValues["id"] as string);

                DataContext dataContetx = context.RequestServices.GetRequiredService<DataContext>();

                Product product = dataContetx?.Products?.Find(key);

                if (product is null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                }
                else
                {
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(JsonSerializer.Serialize(product));
                }

            });

            endpoint.MapGet(BASEURL, async context =>
            {
                DataContext data = context.RequestServices.GetService<DataContext>();
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize<IEnumerable<Product>>(data.Products));
            });

            endpoint.MapPost(BASEURL, async context => {
                DataContext data = context.RequestServices.GetService<DataContext>();
                Product p = await JsonSerializer.DeserializeAsync<Product>(context.Request.Body);
                await data.AddAsync(p);
                await data.SaveChangesAsync();
                context.Response.StatusCode = StatusCodes.Status200OK;
            });
        }
    }
}

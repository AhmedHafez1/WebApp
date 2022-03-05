using Data;

namespace WebApp
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _nextDelegate;

        public TestMiddleware(RequestDelegate nextDelegate)
        {
            _nextDelegate = nextDelegate;
        }

        public async Task Invoke (HttpContext httpContext, DataContext dataContext)
        {
            if (httpContext.Request.Path == "/test")
            {
                await httpContext.Response.WriteAsync($"There are {dataContext?.Suppliers?.Count()} Suppliers \n" );
                await httpContext.Response.WriteAsync($"There are {dataContext?.Products?.Count()} Products \n");
                await httpContext.Response.WriteAsync($"There are {dataContext?.Categories?.Count()} Categories \n");
            } else
            {
                await _nextDelegate(httpContext);
            }

        }
    }
}

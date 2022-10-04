/// <summary>
/// Middleware to allow CORS
/// </summary>
public class CorsMiddleware
{
    private readonly RequestDelegate _next;

    public CorsMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
        context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
        context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
        
        if(context.Request.Method == "OPTIONS")
        {
            context.Response.Headers.Add("Allow", "GET, POST, PUT, DELETE, OPTIONS");
            context.Response.StatusCode = 200;
            return;
        }
        
        await _next(context);
    }
}

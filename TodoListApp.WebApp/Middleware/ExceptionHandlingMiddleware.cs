namespace TodoListApp.WebApp.Middleware;
using System.Net;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
#pragma warning disable CA1031
        catch (Exception ex)
#pragma warning restore CA1031
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        string redirectUrl;

        switch (exception)
        {
            case HttpRequestException ex:
                redirectUrl = ex.StatusCode switch
                {
                    HttpStatusCode.Unauthorized => "/Auth/Login",
                    HttpStatusCode.NotFound => "/Error/NotFound",
                    HttpStatusCode.BadRequest => "/Error/BadRequest",
                    _ => "/Error/Server"
                };
                break;

            default:
                redirectUrl = "/Error/Server";
                break;
        }

        context.Response.Redirect(redirectUrl);
        return Task.CompletedTask;
    }
}

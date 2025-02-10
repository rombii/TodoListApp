namespace TodoListApp.WebApi.Exceptions;

public class SessionExpiredException : Exception
{
    public SessionExpiredException(string message)
        : base(message)
    {
    }

    public SessionExpiredException()
    {
    }

    public SessionExpiredException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

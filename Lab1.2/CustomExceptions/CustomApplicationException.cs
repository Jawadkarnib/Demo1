using System;

public class CustomApplicationException : ApplicationException
{
    public int StatusCode { get; }

    public CustomApplicationException(string message, int statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public CustomApplicationException(string message, Exception innerException, int statusCode)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }
}
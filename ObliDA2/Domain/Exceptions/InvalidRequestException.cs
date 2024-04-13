using System;

namespace Domain.Exceptions;

public class InvalidRequestException : Exception
{
    public InvalidRequestException()
        : base("Invalid request ID provided.")
    {
    }

    public InvalidRequestException(string message)
        : base(message)
    {
    }
}
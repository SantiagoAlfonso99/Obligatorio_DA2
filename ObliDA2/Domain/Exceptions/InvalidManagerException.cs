using System;

namespace Domain.Exceptions;

public class InvalidManagerException : Exception
{
    public InvalidManagerException()
        : base("Invalid manager ID provided.")
    {
    }

    public InvalidManagerException(string message)
        : base(message)
    {
    }
}
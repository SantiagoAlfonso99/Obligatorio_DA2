namespace Domain.Exceptions;

public class EmptyOrNullException : Exception
{
    public EmptyOrNullException() : base("Please make sure not to input null or empty values.") { }
}
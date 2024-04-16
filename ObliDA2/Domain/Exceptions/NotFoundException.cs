namespace Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException() : base( "The action cannot be performed because the element was not found.") { }
}
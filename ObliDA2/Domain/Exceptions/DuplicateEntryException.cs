namespace Domain.Exceptions;

public class DuplicateEntryException : Exception
{
    public DuplicateEntryException() : base("Please ensure not to input duplicate unique values.") { }
}
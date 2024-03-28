using BusinessLogic.Exceptions;
namespace BusinessLogic.Models;

public abstract class User
{
    private string name;
    
    public int Id { get; set; }
    public string Name
    {
        get => name;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidUserException();
            }
        }
    }
}
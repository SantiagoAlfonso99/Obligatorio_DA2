using BusinessLogic.Exceptions;
namespace BusinessLogic.Models;

public abstract class User
{
    private string name;
    private string password;
    
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
            name = value;
        }
    }
    
    public string Password
    {
        get => password;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidUserException();
            }
            password = value;
        }
    }
}
using BusinessLogic.Exceptions;
namespace BusinessLogic.Models;

public class Admin : User
{
    private string lastName;
    public string LastName
    {
        get => lastName;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new InvalidAdminException();
            }
            lastName = value;
        }
    }
    
}
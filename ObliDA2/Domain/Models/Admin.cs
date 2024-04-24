using Domain.Exceptions;
namespace Domain.Models;

public class Admin : User
{
    public string lastName;
    public string LastName
    {
        get => lastName;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new EmptyOrNullException();
            }
            lastName = value;
        }
    }
    
    public bool AreEqual(Admin otherAdmin)
    {
        if (otherAdmin == null)
        {
            return false;
        }
        return this.Name == otherAdmin.Name 
               && this.Password == otherAdmin.Password
               && this.Id == otherAdmin.Id
               && this.Email == otherAdmin.Email
               && this.LastName == otherAdmin.LastName;
    }
    
}
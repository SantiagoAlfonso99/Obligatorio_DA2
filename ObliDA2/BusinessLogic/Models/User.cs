using System.Text.RegularExpressions;
using BusinessLogic.Exceptions;
namespace BusinessLogic.Models;

public abstract class User
{
    private string name;
    private string password;
    private string email;
    
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
    
    public string Email
    {
        get => email;
        set 
        {
            if (!IsEmailValid(value))
            {
                throw new InvalidUserException();
            } 
            email = value;
        }
    } 
    
    private bool IsEmailValid(string emailInput)
    {
        return !string.IsNullOrEmpty(emailInput) && Regex.IsMatch(emailInput, @"^[a-zA-Z0-9]+@[a-zA-Z]+\.com$" );
    }
    
    public bool AreEqual(User otherUser)
    {
        if (otherUser == null)
        {
            return false;
        }
        return this.Name == otherUser.Name 
               && this.Password == otherUser.Password
               && this.Id == otherUser.Id
               && this.Email == otherUser.Email;
    }
}
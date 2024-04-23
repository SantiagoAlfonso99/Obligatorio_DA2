using System.Text.RegularExpressions;
using Domain.Exceptions;
namespace Domain.Models;

public abstract class User
{
    private const string RegexMatch = @"^[a-zA-Z0-9]+@[a-zA-Z]+\.com$";
    
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
                throw new EmptyOrNullException();
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
                throw new EmptyOrNullException();
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
                throw new ArgumentException();
            } 
            email = value;
        }
    } 
    
    private bool IsEmailValid(string emailInput)
    {
        return !string.IsNullOrEmpty(emailInput) && Regex.IsMatch(emailInput, RegexMatch);
    }
}
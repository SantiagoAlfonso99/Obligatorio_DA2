using Domain.Exceptions;
using System.Text.RegularExpressions;

namespace Domain.Models;

public class ApartmentOwner
{
    private const string RegexMatch = @"^[a-zA-Z0-9]+@[a-zA-Z]+\.com$";
    
    private string name;
    private string lastName;
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
    public string Email
    {
        get => email;
        set
        {
            if (!IsEmailValid(value))
            {
                throw new EmptyOrNullException();
            }
            email = value;
        }
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        ApartmentOwner other = (ApartmentOwner)obj;
        return Id == other.Id
               && Name == other.Name
               && LastName == other.LastName
               && Email == other.Email;
    }
    
    private bool IsEmailValid(string emailInput)
    {
        return !string.IsNullOrEmpty(emailInput) && Regex.IsMatch(emailInput, RegexMatch);
    }
}
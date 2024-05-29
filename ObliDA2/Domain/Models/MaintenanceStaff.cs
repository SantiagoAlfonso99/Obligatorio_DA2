using System.Collections;
using Domain.Exceptions;

namespace Domain.Models;

public class MaintenanceStaff : User
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
    public virtual ICollection<Building> Buildings {get; set; }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        MaintenanceStaff other = (MaintenanceStaff)obj;
        return Id == other.Id
               && LastName == other.LastName
               && Name == other.Name
               && Password == other.Password
               && Email == other.Email; 
    }
    
    public override string GetRole()
    {
        return "Maintenance";
    }
}
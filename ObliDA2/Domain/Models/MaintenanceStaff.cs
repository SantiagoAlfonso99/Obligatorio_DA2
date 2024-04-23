using Domain.Exceptions;

namespace Domain.Models;

public class MaintenanceStaff : User
{
    public override string Role => "maintenance";

    private string lastName;
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
    public Building AssociatedBuilding { get; set; }
    
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
               && Email == other.Email
               && AssociatedBuilding == other.AssociatedBuilding;
    }
}
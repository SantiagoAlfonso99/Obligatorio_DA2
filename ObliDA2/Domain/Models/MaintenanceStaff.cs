namespace Domain.Models;

public class MaintenanceStaff : User
{
    public string LastName { get; set; }
    
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
}
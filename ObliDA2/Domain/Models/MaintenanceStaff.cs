namespace Domain.Models;

public class MaintenanceStaff
{
    public int Id { get; set; }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        MaintenanceStaff other = (MaintenanceStaff)obj;
        return Id == other.Id;
    }
}
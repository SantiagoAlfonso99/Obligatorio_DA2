namespace Domain.Models;
public class Request
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int? DepartmentId { get; set; }
    public virtual Apartment Department { get; set; }
    public virtual Category Category { get; set; }
    public RequestStatus Status { get; set; }
    public DateTime Service_start { get; set; }
    public DateTime Service_end { get; set; }
    public int? AssignedToMaintenanceId { get; set; } 
    public virtual MaintenanceStaff AssignedToMaintenance { get; set; }
    public int BuildingAssociatedId { get; set; }
    public virtual Building BuildingAssociated { get; set; }
    
    public override bool Equals(object obj)
    {
        Request other = (Request)obj;
        return Id == other.Id
               && Category.Name == other.Category.Name
               && Description == other.Description
               && Status == other.Status;
    }
}

public enum RequestStatus
{
    Open,       
    Attending,  
    Closed      
}


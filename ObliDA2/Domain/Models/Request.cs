namespace Domain.Models;
public class Request
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Department { get; set; }
    public string Category { get; set; }
    public RequestStatus Status { get; set; }
    public DateTime service_start { get; set; }
    public DateTime service_end { get; set; }
    public int? AssignedToMaintenanceId { get; set; } 
}

public enum RequestStatus
{
    Open,       
    Attending,  
    Closed      
}


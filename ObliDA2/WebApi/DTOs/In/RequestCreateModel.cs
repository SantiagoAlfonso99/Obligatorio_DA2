using Domain.Models;

namespace WebApi.DTOs.In;

public class RequestCreateModel
{
    public int Id { get; set; }
    public string Description { get; set; }
    public Apartment Department { get; set; }
    public string Category { get; set; }
    public RequestStatus Status { get; set; }
    public DateTime Service_start { get; set; }
    public DateTime Service_end { get; set; }
    public int? AssignedToMaintenanceId { get; set; } 
    public int BuildingAssociatedId { get; set; }
    
    public Request ToEntity()
    {
        return new Request()
        {
            Id = this.Id,
            Description = this.Description,
            Department = this.Department,
            Category = this.Category,
            Status = this.Status,
            Service_start = default,
            Service_end = default,
            AssignedToMaintenanceId = this.AssignedToMaintenanceId,
            BuildingAssociatedId = this.BuildingAssociatedId,
        };
    }
}
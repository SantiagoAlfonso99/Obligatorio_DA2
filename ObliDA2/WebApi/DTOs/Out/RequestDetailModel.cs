using Domain.Models;

namespace WebApi.DTOs.Out;
public class RequestDetailModel
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

    public RequestDetailModel(Request request)
    {
        Id = request.Id;
        Description = request.Description;
        Department = request.Department;
        Category = request.Category;
        Status = request.Status;
        Service_start = default;
        Service_end = default;
        AssignedToMaintenanceId = this.AssignedToMaintenanceId;
        BuildingAssociatedId = this.BuildingAssociatedId;
    }
}
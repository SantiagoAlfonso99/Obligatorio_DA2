using Domain.Models;

namespace WebApi.DTOs.Out;

public class MaintenanceStaffResponse
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public DateTime? Service_Start { get; set; }
    public DateTime? Service_End { get; set; }

    public MaintenanceStaffResponse(Request request)
    {
        Id = request.Id;
        Description = request.Description;
        Status = request.Status.ToString();
        Service_Start = request.Service_start;
        Service_End = request.Service_end;
    }
    
    public override bool Equals(object obj)
    {
        MaintenanceStaffResponse other = (MaintenanceStaffResponse)obj;
        return Id == other.Id
               && Description == other.Description
               && Status == other.Status;
    }
}
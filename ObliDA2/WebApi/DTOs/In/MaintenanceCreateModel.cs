using Domain.Models;

namespace WebApi.DTOs.In;

public class MaintenanceCreateModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int AssociatedBuildingId { get; set; }
    
    public MaintenanceStaff ToEntity()
    {
        return new MaintenanceStaff()
        {
            Name = this.Name,
            LastName = this.LastName,
            Password = this.Password,
            Email = this.Email,
        };
    }
    
}
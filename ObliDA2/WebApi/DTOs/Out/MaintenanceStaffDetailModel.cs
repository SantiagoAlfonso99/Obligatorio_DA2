using Domain.Models;

namespace WebApi.DTOs.Out;

public class MaintenanceStaffDetailModel
{
    public MaintenanceStaffDetailModel(MaintenanceStaff staffIn)
    {
        Id = staffIn.Id;
        Name = staffIn.Name;
        LastName = staffIn.LastName;
        Password = staffIn.Password;
        Email = staffIn.Email;
        AssociatedBuilding = staffIn.AssociatedBuilding;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Building AssociatedBuilding { get; set; }
    
    public override bool Equals(object? obj)
    {
        var otherStaffDetail = obj as MaintenanceStaffDetailModel;
        return Id == otherStaffDetail.Id
            && Name == otherStaffDetail.Name
            && LastName == otherStaffDetail.LastName
            && Password == otherStaffDetail.Password
            && Email == otherStaffDetail.Email
            && AssociatedBuilding == otherStaffDetail.AssociatedBuilding;
    }
}
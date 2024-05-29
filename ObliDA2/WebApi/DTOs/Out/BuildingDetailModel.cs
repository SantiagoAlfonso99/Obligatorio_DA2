using Domain.Models;

namespace WebApi.DTOs.Out;

public class BuildingDetailModel
{
    public BuildingDetailModel(Building buildingIn)
    {
        ManagerName = buildingIn.BuildingManager == null ? "" : buildingIn.BuildingManager.Name;
        Id = buildingIn.Id;
        Name = buildingIn.Name;
        Address = buildingIn.Address;
        Latitude = buildingIn.Latitude;
        Longitude = buildingIn.Longitude;
        ConstructionCompany = buildingIn.Company.Name;
        CommonExpenses = buildingIn.CommonExpenses;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string ConstructionCompany { get; set; }
    public int CommonExpenses { get; set; }
    public string ManagerName { get; set; }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        BuildingDetailModel other = (BuildingDetailModel)obj;
        return Id == other.Id
               && Name == other.Name
               && Address == other.Address
               && Longitude.Equals(other.Longitude)
               && Latitude.Equals(other.Latitude)
               && ConstructionCompany == other.ConstructionCompany
               && CommonExpenses == other.CommonExpenses
               && ManagerName == other.ManagerName;
    }
}
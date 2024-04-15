using Domain.Models;

namespace WebApi.DTOs.Out;

public class BuildingDetailModel
{
    public BuildingDetailModel(Building buildingIn)
    {
        Id = buildingIn.Id;
        Name = buildingIn.Name;
        Address = buildingIn.Address;
        Location = buildingIn.Location;
        ConstructionCompany = buildingIn.ConstructionCompany;
        CommonExpenses = buildingIn.CommonExpenses;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Location { get; set; }
    public string ConstructionCompany { get; set; }
    public int CommonExpenses { get; set; }
    
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
               && Location == other.Location
               && ConstructionCompany == other.ConstructionCompany
               && CommonExpenses == other.CommonExpenses;
    }
}
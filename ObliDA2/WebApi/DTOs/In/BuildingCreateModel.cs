using Domain.Models;

namespace WebApi.DTOs.In;

public class BuildingCreateModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string ConstructionCompany { get; set; }
    public int CommonExpenses { get; set; }

    public Building ToEntity()
    {
        return new Building()
        {
            Name = this.Name,
            Address = this.Address,
            Latitude = this.Latitude,
            Longitude = this.Longitude,
            ConstructionCompany = this.ConstructionCompany,
            CommonExpenses = this.CommonExpenses
        };
    }
}
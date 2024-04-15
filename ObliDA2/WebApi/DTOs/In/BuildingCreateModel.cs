using Domain.Models;

namespace WebApi.DTOs.In;

public class BuildingCreateModel
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Location { get; set; }
    public string ConstructionCompany { get; set; }
    public int CommonExpenses { get; set; }

    public Building ToEntity()
    {
        return new Building()
        {
            Name = this.Name,
            Address = this.Address,
            Location = this.Location,
            ConstructionCompany = this.ConstructionCompany,
            CommonExpenses = this.CommonExpenses
        };
    }
}
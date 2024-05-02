using Domain.Models;

namespace WebApi.DTOs.In;

public class BuildingUpdateModel
{
    public string ConstructionCompany { get; set; }
    public int CommonExpenses { get; set; }

    public Building ToEntity()
    {
        return new Building()
        {
            ConstructionCompany = this.ConstructionCompany,
            CommonExpenses = this.CommonExpenses
        };
    }
    
}
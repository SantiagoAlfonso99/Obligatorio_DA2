using Domain.Models;

namespace WebApi.DTOs.In;

public class BuildingUpdateModel
{
    public int CommonExpenses { get; set; }

    public Building ToEntity()
    {
        return new Building()
        {
            CommonExpenses = this.CommonExpenses
        };
    }
    
}
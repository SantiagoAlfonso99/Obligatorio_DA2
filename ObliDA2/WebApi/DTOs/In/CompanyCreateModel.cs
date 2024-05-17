using Domain.Models;

namespace WebApi.DTOs.In;

public class CompanyCreateModel
{
    public string Name { get; set; }
    
    public ConstructionCompany ToEntity()
    {
        return new ConstructionCompany()
        {
            Name = this.Name
        };
    }
}
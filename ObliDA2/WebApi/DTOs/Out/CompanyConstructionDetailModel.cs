using Domain.Models;

namespace WebApi.DTOs.Out;

public class CompanyConstructionDetailModel
{
    public CompanyConstructionDetailModel(ConstructionCompany company)
    {
        Name = company.Name;
    }
    
    public string Name { get; set; }
}
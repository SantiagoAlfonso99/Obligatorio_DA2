using Domain.Models;

namespace IImporter;

public class BuildingImporterDTO
{
    public string Name { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int CommonExpenses { get; set; }
    public ManagerImporterDTO Manager { get; set; }
    public List<ApartmentImporterDTO> Apartments { get; set; }

    public Building ToEntity(ConstructionCompany company)
    {
        return new Building()
        {
            Name = this.Name,
            Address = this.Address,
            Latitude = this.Latitude,
            Longitude = this.Longitude,
            CommonExpenses = this.CommonExpenses,
            Company = company
        };
    }
}
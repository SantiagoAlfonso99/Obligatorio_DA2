using IImporter;
namespace Tests.ControllersTests;

public class ImporterStub : ImporterInterface
{
    public string GetName()
    {
        return "importerStub";
    }

    public List<BuildingImporterDTO> ImportBuildings()
    {
        List<BuildingImporterDTO> returnedBuildings = new List<BuildingImporterDTO>();
        ManagerImporterDTO manager1 = new ManagerImporterDTO()
            { Name = "Manager1", Password = "Password1", Email = "Manager1@gmail.com" };
        OwnerImporterDTO owner1 = new OwnerImporterDTO()
            { Name = "Owner1", LastName = "Owner1", Email = "Owner1@gmail.com" };
        ApartmentImporterDTO apartment1 = new ApartmentImporterDTO()
        {
            NumberOfBathroom = 3, NumberOfBedrooms = 4,
            Floor = 3,  Terrace = true,
            Owner = owner1, Number = 76
        };
        ApartmentImporterDTO apartment2 = new ApartmentImporterDTO()
        {
            NumberOfBathroom = 4, NumberOfBedrooms = 5,
            Floor = 3,  Terrace = true,
            Owner = owner1, Number = 77
        };
        BuildingImporterDTO building1 = new BuildingImporterDTO()
        {
            Address = "Address1", CommonExpenses = 5, Latitude = 5, Longitude = 5, Name = "Building1",
            Apartments = new List<ApartmentImporterDTO>(){apartment1, apartment2},
            Manager = manager1
        };
        returnedBuildings.Add(building1);
        return returnedBuildings;
    }
}
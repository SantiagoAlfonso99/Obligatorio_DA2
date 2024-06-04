using BusinessLogic.IRepository;
using BusinessLogic.Services;
using Domain.Models;
using IImporter;
using Moq;

namespace Tests.ServicesTests;

[TestClass]
public class ImporterLogicTests
{
   [TestMethod]
   public void ImportBuildingsOk()
   {
      Mock<IBuildingRepository> buildingRepo = new Mock<IBuildingRepository>();
      Mock<IApartmentRepository> apartmentRepo = new Mock<IApartmentRepository>();
      Mock<IApartmentOwnerRepository> apartmentOwnerRepo = new Mock<IApartmentOwnerRepository>();
      Mock<IManagerRepository> managerRepo = new Mock<IManagerRepository>();
      ImporterLogic importerLogic = new ImporterLogic(buildingRepo.Object, apartmentRepo.Object, managerRepo.Object, apartmentOwnerRepo.Object);
      List<BuildingImporterDTO> importedBuildings = SetupDtos();
      List<Building> expectedBuildings = ExpectedBuildings();
      ConstructionCompany newCompany = new ConstructionCompany() { Id = 1, Name = "Company1" };
      buildingRepo.Setup(repo => repo.Create(It.IsAny<Building>()));
      apartmentRepo.Setup(repo => repo.Create(It.IsAny<Apartment>()));
      apartmentOwnerRepo.Setup(repo => repo.Create(It.IsAny<ApartmentOwner>()));
      managerRepo.Setup(repo => repo.Add(It.IsAny<Manager>()));
      
      List<Building> returnedBuildings = importerLogic.ReturnBuildings(importedBuildings, newCompany);
      
      buildingRepo.VerifyAll();
      apartmentOwnerRepo.VerifyAll();
      apartmentRepo.VerifyAll();
      managerRepo.VerifyAll();
      CollectionAssert.AreEqual(expectedBuildings, returnedBuildings);
   }

   private List<BuildingImporterDTO> SetupDtos()
   {
      List<BuildingImporterDTO> returnedBuildings = new List<BuildingImporterDTO>();
      ManagerImporterDTO manager1 = new ManagerImporterDTO()
         { Name = "Manager1", Password = "Password1", Email = "Manager1@gmail.com" };
      ManagerImporterDTO manager2 = new ManagerImporterDTO()
         { Name = "Manager2", Password = "Password2", Email = "Manager2@gmail.com" };
      OwnerImporterDTO owner1 = new OwnerImporterDTO()
         { Name = "Owner1", LastName = "Owner1", Email = "Owner1@gmail.com" };
      OwnerImporterDTO owner2 = new OwnerImporterDTO()
         { Name = "Owner2", LastName = "Owner2", Email = "Owner2@gmail.com" };
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
      ApartmentImporterDTO apartment3 = new ApartmentImporterDTO()
      {
         NumberOfBathroom = 5, NumberOfBedrooms = 6,
         Floor = 3,  Terrace = true,
         Owner = owner2, Number = 78
      };
      ApartmentImporterDTO apartment4 = new ApartmentImporterDTO()
      {
         NumberOfBathroom = 6, NumberOfBedrooms = 7,
         Floor = 3,  Terrace = true,
         Owner = owner2, Number = 79
      };
      BuildingImporterDTO building1 = new BuildingImporterDTO()
      {
         Address = "Address1", CommonExpenses = 5, Latitude = 5, Longitude = 5, Name = "Building1",
         Apartments = new List<ApartmentImporterDTO>(){apartment1, apartment2},
         Manager = manager1
      };
      BuildingImporterDTO building2 = new BuildingImporterDTO()
      {
         Address = "Address2", CommonExpenses = 5, Latitude = 6, Longitude = 6, Name = "Building2",
         Apartments = new List<ApartmentImporterDTO>(){apartment3, apartment4},
         Manager = manager2
      };
      returnedBuildings.Add(building1);
      returnedBuildings.Add(building2);
      return returnedBuildings;
   }
   
   private List<Building> ExpectedBuildings()
   {
      List<Building> returnedBuildings = new List<Building>();
      Manager manager1 = new Manager()
         { Name = "Manager1", Password = "Password1", Email = "Manager1@gmail.com" };
      Manager manager2 = new Manager()
         { Name = "Manager2", Password = "Password2", Email = "Manager2@gmail.com" };
      Building building1 = new Building()
      {
         Address = "Address1", CommonExpenses = 5, Latitude = 5, Longitude = 5, Name = "Building1",
         BuildingManager = manager1, Company = new ConstructionCompany(){Id =1, Name = "Company1"}
      };
      Building building2 = new Building()
      {
         Address = "Address2", CommonExpenses = 5, Latitude = 6, Longitude = 6, Name = "Building2",
         BuildingManager = manager2, Company = new ConstructionCompany(){Id =1, Name = "Company1"}
      };
      returnedBuildings.Add(building1);
      returnedBuildings.Add(building2);
      return returnedBuildings;
   }
}
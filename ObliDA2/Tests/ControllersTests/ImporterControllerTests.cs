using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Tests.ControllersTests;
using WebApi.Controllers;
using WebApi.DTOs.Out;
using Moq;
using IBusinessLogic;
using IImporter;

[TestClass]
public class ImporterControllerTests
{
    private Mock<IImporterLogic> importerService;
    private Mock<IUsersLogic> usersService;
    private ImportersController controller;
    private ConstructionCompany company;
    private CompanyAdmin admin;
    
    [TestInitialize]
    public void Initialize()
    {
        importerService = new Mock<IImporterLogic>();
        usersService = new Mock<IUsersLogic>();
        controller = new ImportersController(importerService.Object, usersService.Object);
        company = new ConstructionCompany() { Id = 1, Name = "company1" };
        admin = new CompanyAdmin()
        {
            Name = "name", Email = "Email@gmail.com", Password = "Password",
            Company = company 
        };
    }
    
    
    [TestMethod]
    public void IndexOk()
    {
        ImporterStub stub = new ImporterStub();
        ImportersController controller = new ImportersController(importerService.Object, usersService.Object);
        usersService.Setup(service => service.GetCurrentUser(It.IsAny<Guid?>())).Returns(admin);
        importerService.Setup(service => service.GetAllImporters()).Returns(new List<ImporterInterface>(){stub});

        var result = controller.Index();
        var okResult = result as OkObjectResult;
        var names = okResult.Value as List<string>;
        var expectedNames = new List<string>(){"importerStub"};
        
        CollectionAssert.AreEqual(expectedNames, names);
    }
    
    [TestMethod]
    public void ImportBuildingsOk()
    {
        Building building1 = new Building() {Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000, Company = company, BuildingManager = new Manager(){Id = 1}};
        Building building2 = new Building() {Name = "BuildingName2", Address = "Address2", CommonExpenses = 5, 
            Latitude = 40.001, Longitude = 70.001, Company = company, BuildingManager = new Manager(){Id = 1}};
        usersService.Setup(service => service.GetCurrentUser(It.IsAny<Guid?>())).Returns(admin);
        importerService.Setup(service => service.ImportBuildings(It.IsAny<string>(), It.IsAny<ConstructionCompany>())).Returns(new List<Building>{building1, building2});

        var result = controller.ImportBuildings("importerName");
        var okResult = result as OkObjectResult;
        var returnedBuildings = okResult.Value as List<BuildingDetailModel>;
        var expectedBuildings = new List<BuildingDetailModel>(){new BuildingDetailModel(building1), new BuildingDetailModel(building2)};

        usersService.VerifyAll();
        importerService.VerifyAll();
        CollectionAssert.AreEqual(expectedBuildings, returnedBuildings);
    }
}
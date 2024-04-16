using System.Data.Common;
using Domain.Exceptions;
using Domain.Models;
using BusinessLogic.Services;
using BusinessLogic.IRepository;
using Moq;

namespace Tests.ServicesTests;

[TestClass]
public class BuildingLogicTests
{
    private Mock<IBuildingRepository> repo;
    private Building newBuilding;
    private BuildingLogic service;
    private Building expectedBuilding;
    
    [TestInitialize]
    public void Initialize()
    {
        repo = new Mock<IBuildingRepository>();
        newBuilding = new Building() {Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Location = "Location", ConstructionCompany = "Company"};
        expectedBuilding = new Building() {Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Location = "Location", ConstructionCompany = "Company"};

    }
    
    [TestMethod]
    public void GetAllOk()
    {
        List<Building> buildings = new List<Building>() { newBuilding};
        repo.Setup(repository => repository.GetAll()).Returns(buildings);
        service = new BuildingLogic(repo.Object);

        List<Building> returnedBuildings = service.GetAll();
        List<Building> expectedList = new List<Building>(){ expectedBuilding};
        CollectionAssert.AreEqual(returnedBuildings, expectedList);
    }
    
    [TestMethod]
    public void GetById()
    {
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newBuilding);
        service = new BuildingLogic(repo.Object);

        Building returnedBuilding = service.GetById(1);
        Assert.AreEqual(returnedBuilding, expectedBuilding);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        repo.Setup(repository => repository.Create(It.IsAny<Building>()));
        service = new BuildingLogic(repo.Object);

        Building returnedBuilding = service.Create(newBuilding);
        Assert.AreEqual(returnedBuilding, expectedBuilding);
    }
}
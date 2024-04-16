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
    private const int UserId = 1;
    private const bool DeleteFailed = false;
    private const bool DeleteSuccessfully = true;
    
    [TestInitialize]
    public void Initialize()
    {
        repo = new Mock<IBuildingRepository>();
        newBuilding = new Building() {Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000, ConstructionCompany = "Company"};
        expectedBuilding = new Building() {Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000, ConstructionCompany = "Company"};

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

        Building returnedBuilding = service.GetById(UserId);
        
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
    
    [TestMethod]
    public void DeleteShouldReturnsTrue()
    {
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newBuilding);
        repo.Setup(repository => repository.Delete(It.IsAny<Building>()));
        service = new BuildingLogic(repo.Object);

        bool success = service.Delete(UserId);
        
        Assert.AreEqual(DeleteSuccessfully, success);
    }
    
    [TestMethod]
    public void DeleteShouldReturnsFalse()
    {
        newBuilding = null;
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newBuilding);
        repo.Setup(repository => repository.Delete(It.IsAny<Building>()));
        service = new BuildingLogic(repo.Object);

        bool success = service.Delete(UserId);
        
        Assert.AreEqual( DeleteFailed, success);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newBuilding);
        repo.Setup(repository => repository.Update(It.IsAny<Building>()));
        service = new BuildingLogic(repo.Object);
        Building newAttributes = new Building() { CommonExpenses = 500, ConstructionCompany = "NewCompany" };
        
        Building returnedBuilding = service.Update(UserId, newAttributes);
        expectedBuilding.CommonExpenses = newAttributes.CommonExpenses;
        expectedBuilding.ConstructionCompany = newAttributes.ConstructionCompany;
        Assert.AreEqual( returnedBuilding, expectedBuilding);
    }
    
}
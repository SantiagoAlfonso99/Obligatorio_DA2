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
    private List<Building> buildings;
    private const int UserId = 1;
    private const bool DeleteFailed = false;
    private const bool DeleteSuccessfully = true;
    
    [TestInitialize]
    public void Initialize()
    {
        repo = new Mock<IBuildingRepository>();
        newBuilding = new Building() {Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000, ConstructionCompany = "Company", BuildingManager = new Manager(){Id = 1}};
        expectedBuilding = new Building() {Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000, ConstructionCompany = "Company", BuildingManager = new Manager(){Id = 1}};
        buildings = new List<Building>() { expectedBuilding };
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
    [ExpectedException(typeof(NotFoundException))]
    public void GetByIdThrowsException()
    {
        newBuilding = null;
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newBuilding);
        service = new BuildingLogic(repo.Object);

        Building returnedBuilding = service.GetById(UserId);
        
        Assert.AreEqual(returnedBuilding, expectedBuilding);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        repo.Setup(repository => repository.GetAll()).Returns(new List<Building>());
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
    
    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void UpdateNonExistentElementThrowsException()
    {
        newBuilding = null;
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newBuilding);
        repo.Setup(repository => repository.Update(It.IsAny<Building>()));
        service = new BuildingLogic(repo.Object);
        Building newAttributes = new Building() { CommonExpenses = 500, ConstructionCompany = "NewCompany" };
        
        Building returnedBuilding = service.Update(UserId, newAttributes);
        expectedBuilding.CommonExpenses = newAttributes.CommonExpenses;
        expectedBuilding.ConstructionCompany = newAttributes.ConstructionCompany;
        
        Assert.AreEqual( returnedBuilding, expectedBuilding);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DuplicateEntryException))]
    public void CreatingBuildingWithDuplicateNameShouldThrowException()
    {
        repo.Setup(repository => repository.Create(It.IsAny<Building>()));
        repo.Setup(repository => repository.GetAll()).Returns(buildings);
        service = new BuildingLogic(repo.Object);
        newBuilding.Latitude = 50.001;
        newBuilding.Address = "otherAddress";
        
        Building returnedBuilding = service.Create(newBuilding);
        
        Assert.AreEqual(returnedBuilding, expectedBuilding);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DuplicateEntryException))]
    public void CreatingBuildingWithDuplicateAddressShouldThrowException()
    {
        repo.Setup(repository => repository.Create(It.IsAny<Building>()));
        repo.Setup(repository => repository.GetAll()).Returns(buildings);
        service = new BuildingLogic(repo.Object);
        newBuilding.Latitude = 50.001;
        newBuilding.Name = "otherName";
        
        Building returnedBuilding = service.Create(newBuilding);
        
        Assert.AreEqual(returnedBuilding, expectedBuilding);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DuplicateEntryException))]
    public void CreatingBuildingWithDuplicateLocationShouldThrowException()
    {
        repo.Setup(repository => repository.Create(It.IsAny<Building>()));
        repo.Setup(repository => repository.GetAll()).Returns(buildings);
        service = new BuildingLogic(repo.Object);
        newBuilding.Address = "otherAddress";
        newBuilding.Name = "otherName";
        
        Building returnedBuilding = service.Create(newBuilding);
        
        Assert.AreEqual(returnedBuilding, expectedBuilding);
    }
    
    [TestMethod]
    [ExpectedException(typeof(EmptyOrNullException))]
    public void CreatingBuildingWithEmptyNameThrowException()
    {
        newBuilding.Name = "";
        Building returnedBuilding = service.Create(newBuilding);
    }
    
    [TestMethod]
    [ExpectedException(typeof(EmptyOrNullException))]
    public void CreatingBuildingWithEmptyAddressThrowException()
    {
        newBuilding.Address = "";
        Building returnedBuilding = service.Create(newBuilding);
    }
    
    [TestMethod]
    [ExpectedException(typeof(EmptyOrNullException))]
    public void CreatingBuildingWithEmptyCompanyThrowException()
    {
        newBuilding.ConstructionCompany = "";
        Building returnedBuilding = service.Create(newBuilding);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreatingBuildingWithNegativeCommonExpensesThrowException()
    {
        newBuilding.CommonExpenses = -5;
        Building returnedBuilding = service.Create(newBuilding);
    }
}
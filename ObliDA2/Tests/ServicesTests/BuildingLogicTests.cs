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
        ConstructionCompany company = new ConstructionCompany() { Name = "Company" };
        repo = new Mock<IBuildingRepository>();
        newBuilding = new Building() {Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000, Company = company, BuildingManager = new Manager(){Id = 1}};
        expectedBuilding = new Building() {Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000, Company = company, BuildingManager = new Manager(){Id = 1}};
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
        newBuilding.BuildingManagerId = 1;
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newBuilding);
        repo.Setup(repository => repository.Delete(It.IsAny<Building>()));
        service = new BuildingLogic(repo.Object);

        bool success = service.Delete(UserId, 1);
        
        Assert.AreEqual(DeleteSuccessfully, success);
    }
    
    [TestMethod]
    public void DeleteShouldReturnsFalse()
    {
        newBuilding = null;
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newBuilding);
        repo.Setup(repository => repository.Delete(It.IsAny<Building>()));
        service = new BuildingLogic(repo.Object);

        bool success = service.Delete(UserId, 1);
        
        Assert.AreEqual( DeleteFailed, success);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        newBuilding.BuildingManagerId = 1;
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newBuilding);
        repo.Setup(repository => repository.Update(It.IsAny<Building>()));
        service = new BuildingLogic(repo.Object);
        Building newAttributes = new Building() { CommonExpenses = 500};
        
        Building returnedBuilding = service.Update(UserId, newAttributes, 1);
        expectedBuilding.CommonExpenses = newAttributes.CommonExpenses;
        
        Assert.AreEqual( returnedBuilding, expectedBuilding);
    }
    
    [TestMethod]
    public void UpdateManagerOk()
    {
        repo.Setup(repository => repository.Update(It.IsAny<Building>()));
        service = new BuildingLogic(repo.Object);
        Building newAttributes = new Building() {Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000, Company = new ConstructionCompany(){Id =1, Name ="C1"}, BuildingManager = new Manager(){Id = 1}};
        
        Building returnedBuilding = service.UpdateManager(newAttributes);
        
        Assert.AreEqual( newAttributes.BuildingManager.Id, returnedBuilding.BuildingManager.Id);
    }
    
    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void UpdateThrowsNotFoundExceptionForInvalidManagerId()
    {
        newBuilding.BuildingManagerId = 1;
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newBuilding);
        repo.Setup(repository => repository.Update(It.IsAny<Building>()));
        service = new BuildingLogic(repo.Object);
        Building newAttributes = new Building() { CommonExpenses = 500};
        
        Building returnedBuilding = service.Update(UserId, newAttributes, 2);
        expectedBuilding.CommonExpenses = newAttributes.CommonExpenses;
        
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
        Building newAttributes = new Building() { CommonExpenses = 500};
        
        Building returnedBuilding = service.Update(UserId, newAttributes, 1);
        expectedBuilding.CommonExpenses = newAttributes.CommonExpenses;
        
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
    [ExpectedException(typeof(ArgumentException))]
    public void CreatingBuildingWithNegativeCommonExpensesThrowException()
    {
        newBuilding.CommonExpenses = -5;
        Building returnedBuilding = service.Create(newBuilding);
    }
}
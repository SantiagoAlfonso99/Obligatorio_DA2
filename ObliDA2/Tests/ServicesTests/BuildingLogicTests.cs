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
    [TestMethod]
    public void GetAllOk()
    {
        List<Building> buildings = new List<Building>() { new Building(){Id = 1}};
        Mock<IBuildingRepository> repo = new Mock<IBuildingRepository>();
        repo.Setup(repository => repository.GetAll()).Returns(buildings);
        BuildingLogic service = new BuildingLogic(repo.Object);

        List<Building> returnedBuildings = service.GetAll();
        List<Building> expectedList = new List<Building>(){ new Building(){Id = 1}};
        CollectionAssert.AreEqual(returnedBuildings, expectedList);
    }
    
    [TestMethod]
    public void GetById()
    {
        Mock<IBuildingRepository> repo = new Mock<IBuildingRepository>();
        Building consultedBuilding = new Building() { Id = 1 };
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(consultedBuilding);
        BuildingLogic service = new BuildingLogic(repo.Object);

        Building returnedBuilding = service.GetById(1);
        Building expectedBuilding = new Building() { Id = 1};
        Assert.AreEqual(returnedBuilding, expectedBuilding);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        Mock<IBuildingRepository> repo = new Mock<IBuildingRepository>();
        Building newBuilding = new Building() { Id = 1 };
        repo.Setup(repository => repository.Create(It.IsAny<Building>()));
        BuildingLogic service = new BuildingLogic(repo.Object);

        Building returnedBuilding = service.Create(newBuilding);
        Building expectedBuilding = new Building() { Id = 1};
        Assert.AreEqual(returnedBuilding, expectedBuilding);
    }
}
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
    public void CreateOk()
    {
        List<Building> buildings = new List<Building>() { new Building(){Id = 1}};
        Mock<IBuildingRepository> repo = new Mock<IBuildingRepository>();
        repo.Setup(repository => repository.GetAll()).Returns(buildings);
        BuildingLogic service = new BuildingLogic(repo.Object);

        List<Building> returnedBuildings = service.GetAll();
        List<Building> expectedList = new List<Building>(){ new Building(){Id = 1}};
        CollectionAssert.AreEqual(returnedBuildings, expectedList);
    }
}
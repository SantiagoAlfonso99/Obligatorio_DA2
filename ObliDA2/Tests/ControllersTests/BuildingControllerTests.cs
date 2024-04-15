using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;
using Building = Domain.Models.Building;

namespace Tests.ControllersTests;

[TestClass]
public class BuildingControllerTests
{
    [TestMethod]
    public void GetAllOk()
    {
        Mock<IBuildingLogic> service = new Mock<IBuildingLogic>();
        List<Building> buildings = new List<Building>(){new Building(){Id = 1}};
        service.Setup(logic => logic.GetAll()).Returns(buildings);
        BuildingController controller = new BuildingController(service.Object);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<Building> returnedBuilding = okResult.Value as List<Building>;
        List<Building> expectedModels = new List<Building>(){new Building(){Id = 1}};

        service.VerifyAll();
        CollectionAssert.AreEqual(returnedBuilding, expectedModels);
    }
    
    [TestMethod]
    public void ShowOk()
    {
        Mock<IBuildingLogic> service = new Mock<IBuildingLogic>();
        Building consultedBuilding = new Building(){Id = 1};
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(consultedBuilding);
        BuildingController controller = new BuildingController(service.Object);
        
        var result = controller.Show(1);
        var okResult = result as OkObjectResult;
        Building returnedBuilding = okResult.Value as Building;
        Building expectedModel = new Building(){Id = 1};

        service.VerifyAll();
        Assert.AreEqual(expectedModel, returnedBuilding);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        Mock<IBuildingLogic> service = new Mock<IBuildingLogic>();
        Building newBuilding = new Building(){Id = 1};
        service.Setup(logic => logic.Create(It.IsAny<Building>())).Returns(newBuilding);
        BuildingController controller = new BuildingController(service.Object);
        
        var result = controller.Create(new Building(){Id = 1});
        var okResult = result as OkObjectResult;
        Building returnedBuilding = okResult.Value as Building;
        Building expectedModel = new Building(){Id = 1};

        service.VerifyAll();
        Assert.AreEqual(expectedModel, returnedBuilding);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Mock<IBuildingLogic> service = new Mock<IBuildingLogic>();
        service.Setup(logic => logic.Delete(It.IsAny<Building>())).Returns(true);
        BuildingController controller = new BuildingController(service.Object);
        
        var result = controller.Delete(new Building(){Id = 1});
        var noContentResult = result as NoContentResult;

        service.VerifyAll();
        Assert.IsNotNull(noContentResult);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        Mock<IBuildingLogic> service = new Mock<IBuildingLogic>();
        service.Setup(logic => logic.Update(It.IsAny<int>(),It.IsAny<Building>())).Returns(new Building(){Id = 2});
        BuildingController controller = new BuildingController(service.Object);
        
        var result = controller.Update(1, new Building(){Id = 2});
        var okResult = result as OkObjectResult;
        Building returnedBuilding = okResult.Value as Building;
        Building expectedModel = new Building(){Id = 2};

        service.VerifyAll();
        Assert.AreEqual(expectedModel, returnedBuilding);
    }
}
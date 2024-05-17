using BusinessLogic.IRepository;
using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using WebApi.DTOs.Out;
using WebApi.DTOs.In;
using Moq;
using Building = Domain.Models.Building;

namespace Tests.ControllersTests;

[TestClass]
public class BuildingControllerTests
{
    private Mock<IBuildingLogic> service;
    private Mock<IManagerLogic> managerService;
    private Building newBuilding;
    private Building expectedModel;
    private const int UserId = 1;
    private const string PropertyName = "Message";
    private const string NotFoundMessage =
        "The deletion action could not be completed because there is no Building with that ID";
    
        
    [TestInitialize]
    public void Initialize()
    {
        managerService = new Mock<IManagerLogic>();
        service = new Mock<IBuildingLogic>();
        newBuilding = new Building() { Id = UserId };
        expectedModel = new Building() { Id = UserId };
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        newBuilding.BuildingManager = new Manager()
            { Email = "peep@gmail.com", Id = 2, Name = "pepe", Password = "pepe" };
        List<Building> buildings = new List<Building>(){newBuilding};
        service.Setup(logic => logic.GetAll()).Returns(buildings);
        BuildingController controller = new BuildingController(service.Object, managerService.Object);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<BuildingDetailModel> returnedBuilding = okResult.Value as List<BuildingDetailModel>;
        expectedModel.BuildingManager = new Manager()
            { Email = "peep@gmail.com", Id = 2, Name = "pepe", Password = "pepe" };
        List<BuildingDetailModel> expectedModels = new List<BuildingDetailModel>(){new BuildingDetailModel(expectedModel)};

        service.VerifyAll();
        CollectionAssert.AreEqual(returnedBuilding, expectedModels);
    }
    
    [TestMethod]
    public void ShowOk()
    {
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(newBuilding);
        BuildingController controller = new BuildingController(service.Object, managerService.Object);
        
        var result = controller.Show(UserId);
        var okResult = result as OkObjectResult;
        BuildingDetailModel returnedBuilding = okResult.Value as BuildingDetailModel;
        BuildingDetailModel expectedModelDetail = new BuildingDetailModel(newBuilding);

        service.VerifyAll();
        Assert.AreEqual(expectedModelDetail, returnedBuilding);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        service.Setup(logic => logic.Create(It.IsAny<Building>())).Returns(newBuilding);
        managerService.Setup(service => service.GetById(It.IsAny<int>())).Returns(new Manager() { Id = 1 });
        BuildingController controller = new BuildingController(service.Object, managerService.Object);
        
        var result = controller.Create(new BuildingCreateModel(){Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000, ConstructionCompany = "Company"});
        var okResult = result as OkObjectResult;
        BuildingDetailModel returnedBuilding = okResult.Value as BuildingDetailModel;
        BuildingDetailModel expectedModelDetail = new BuildingDetailModel(newBuilding);

        service.VerifyAll();
        Assert.AreEqual(expectedModelDetail, returnedBuilding);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(true);
        BuildingController controller = new BuildingController(service.Object, managerService.Object);
        
        var result = controller.Delete(UserId);
        var noContentResult = result as NoContentResult;

        service.VerifyAll();
        Assert.IsNotNull(noContentResult);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        service.Setup(logic => logic.Update(It.IsAny<int>(),It.IsAny<Building>())).Returns(new Building(){Id = UserId});
        BuildingController controller = new BuildingController(service.Object, managerService.Object);
        BuildingUpdateModel newAttributes = new BuildingUpdateModel() {CommonExpenses = 500, ConstructionCompany = "Company"};
        
        var result = controller.Update(2, newAttributes);
        var okResult = result as OkObjectResult;
        BuildingDetailModel returnedBuilding = okResult.Value as BuildingDetailModel;
        BuildingDetailModel expectedModelDetail = new BuildingDetailModel(newBuilding);
        
        service.VerifyAll();
        Assert.AreEqual(expectedModelDetail, returnedBuilding);
    }
    
    [TestMethod]
    public void DeleteReturnsFalse()
    {
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(false);
        BuildingController controller = new BuildingController(service.Object, managerService.Object);
        
        var result = controller.Delete(UserId);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty(PropertyName);
        
        service.VerifyAll();
        Assert.AreEqual(NotFoundMessage, message.GetValue(notFoundResult.Value));
    }
}
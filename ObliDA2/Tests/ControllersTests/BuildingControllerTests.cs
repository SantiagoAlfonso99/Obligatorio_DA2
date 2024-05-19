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
    private BuildingController controller;
    private Mock<IUsersLogic> usersLogic;    
    
    [TestInitialize]
    public void Initialize()
    {
        managerService = new Mock<IManagerLogic>();
        service = new Mock<IBuildingLogic>();
        newBuilding = new Building() {Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000, Company = new ConstructionCompany(){Id =1, Name ="C1"}, BuildingManager = new Manager(){Id = 1}};
        expectedModel = new Building() {Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000, Company = new ConstructionCompany(){Id =1, Name ="C1"}, BuildingManager = new Manager(){Id = 1}};
        usersLogic = new Mock<IUsersLogic>();
        controller = new BuildingController(service.Object, managerService.Object,usersLogic.Object);
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        var user = new CompanyAdmin()
        {
            Name = "Name", Email = "email@gmail.com", Password = "password",
            Id = 1, Company = new ConstructionCompany() { Id = 2, Name = "Name" }
        };
        Building otherBuilding = new Building() {Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000, Company = new ConstructionCompany(){Id =2, Name ="Name"}, BuildingManager = new Manager(){Id = 1}};
        usersLogic.Setup(logic => logic.GetCurrentUser(It.IsAny<Guid?>())).Returns(user);
        List<Building> buildings = new List<Building>(){newBuilding, otherBuilding};
        service.Setup(logic => logic.GetAll()).Returns(buildings);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<BuildingDetailModel> returnedBuilding = okResult.Value as List<BuildingDetailModel>;
        List<BuildingDetailModel> expectedModels = new List<BuildingDetailModel>(){new BuildingDetailModel(otherBuilding)};

        usersLogic.VerifyAll();
        service.VerifyAll();
        CollectionAssert.AreEqual(expectedModels, returnedBuilding);
    }
    
    [TestMethod]
    public void ShowOk()
    {
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(newBuilding);
        
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
        var user = new CompanyAdmin()
        {
            Name = "Name", Email = "email@gmail.com", Password = "password",
            Id = 1, Company = new ConstructionCompany() { Id = 1, Name = "Name" }
        };
        newBuilding.Company = user.Company;
        usersLogic.Setup(logic => logic.GetCurrentUser(It.IsAny<Guid?>())).Returns(user);
        service.Setup(logic => logic.Create(It.IsAny<Building>())).Returns(newBuilding);
        
        var result = controller.Create(new BuildingCreateModel(){Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000});
        var okResult = result as OkObjectResult;
        BuildingDetailModel returnedBuilding = okResult.Value as BuildingDetailModel;
        BuildingDetailModel expectedModelDetail = new BuildingDetailModel(newBuilding);

        usersLogic.VerifyAll();
        service.VerifyAll();
        Assert.AreEqual(expectedModelDetail.ConstructionCompany, returnedBuilding.ConstructionCompany);
    }
    
    [TestMethod]
    public void CreateReturnsBadRequest()
    {
        var user = new CompanyAdmin()
        {
            Name = "Name", Email = "email@gmail.com", Password = "password",
            Id = 1
        };
        newBuilding.Company = user.Company;
        usersLogic.Setup(logic => logic.GetCurrentUser(It.IsAny<Guid?>())).Returns(user);
        
        var result = controller.Create(new BuildingCreateModel(){Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000});
        var badResult = result as BadRequestObjectResult;
        var message = badResult.Value.GetType().GetProperty(PropertyName);
        
        Assert.AreEqual("The building could not be created because the admin does not have an associated construction company.", message.GetValue(badResult.Value));
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(true);
        
        var result = controller.Delete(UserId);
        var noContentResult = result as NoContentResult;

        service.VerifyAll();
        Assert.IsNotNull(noContentResult);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        newBuilding.CommonExpenses = 500;
        service.Setup(logic => logic.Update(It.IsAny<int>(),It.IsAny<Building>())).Returns(newBuilding);
        BuildingUpdateModel newAttributes = new BuildingUpdateModel() {CommonExpenses = 500};
        
        var result = controller.Update(2, newAttributes);
        var okResult = result as OkObjectResult;
        BuildingDetailModel returnedBuilding = okResult.Value as BuildingDetailModel;
        BuildingDetailModel expectedModelDetail = new BuildingDetailModel(newBuilding);
        
        service.VerifyAll();
        Assert.AreEqual(expectedModelDetail, returnedBuilding);
    }
    
    [TestMethod]
    public void UpdateManagerOk()
    {
        var user = new Manager()
        {
            Name = "Name", Email = "email@gmail.com", Password = "password",
            Id = 1
        };
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(newBuilding);
        managerService.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(user);
        newBuilding.BuildingManager = user;
        service.Setup(logic => logic.UpdateManager(It.IsAny<Building>())).Returns(newBuilding);
        BuildingUpdateManager update = new BuildingUpdateManager() { ManagerId = 1 };        

        var result = controller.UpdateBuildingManager(1, update);
        var okResult = result as OkObjectResult;
        BuildingDetailModel returnedBuilding = okResult.Value as BuildingDetailModel;
        BuildingDetailModel expectedModelDetail = new BuildingDetailModel(newBuilding);
       
        managerService.VerifyAll(); 
        service.VerifyAll();
        Assert.AreEqual(expectedModelDetail, returnedBuilding);
    }
    
    [TestMethod]
    public void DeleteReturnsFalse()
    {
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(false);
        
        var result = controller.Delete(UserId);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty(PropertyName);
        
        service.VerifyAll();
        Assert.AreEqual(NotFoundMessage, message.GetValue(notFoundResult.Value));
    }
}
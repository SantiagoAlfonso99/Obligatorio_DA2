using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;
using Moq;
using MaintenanceStaff = Domain.Models.MaintenanceStaff;

namespace Tests.ControllersTests;

[TestClass]
public class MaintenanceControllerTests
{
    private Mock<IMaintenanceLogic> service;
    private Mock<IBuildingLogic> buildingService;
    private Mock<IUsersLogic> usersService;
    private const int UserId = 1;
    private const string DeleteNotFound = "There is no maintenance staff for that Id";
    private MaintenanceStaff newStaff;
    private MaintenanceStaffController controller;
    private const string PropertyName = "Message";
    private Building newBuilding;
    
    [TestInitialize]
    public void Initialize()
    {
        usersService = new Mock<IUsersLogic>();
        usersService = new Mock<IUsersLogic>();
        buildingService = new Mock<IBuildingLogic>();
        service = new Mock<IMaintenanceLogic>();
        newBuilding = new Building() {Name = "BuildingName", Address = "Address", CommonExpenses = 5, 
            Latitude = 40.000, Longitude = 70.000, Company = new ConstructionCompany(){Id =1, Name ="C1"}, BuildingManager = new Manager(){Id = 1}};
        newStaff = new MaintenanceStaff() { Id = UserId, Name = "pepe", LastName = "rodriguez", Password = "pepe", Email = "pepe@gmail.com", Buildings = new List<Building>(){newBuilding}};
    }
    
    [TestMethod]
    public void IndexOk()
    {
        List<MaintenanceStaff> staff = new List<MaintenanceStaff>() { newStaff };
        service.Setup(logic => logic.GetAll()).Returns(staff);
        controller = new MaintenanceStaffController(service.Object, buildingService.Object, usersService.Object);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<MaintenanceStaffDetailModel> returnedValue = okResult.Value as List<MaintenanceStaffDetailModel>;
        List<MaintenanceStaffDetailModel> expectedList = new List<MaintenanceStaffDetailModel>(){new MaintenanceStaffDetailModel(newStaff)};

        service.VerifyAll();
        CollectionAssert.AreEqual(returnedValue, expectedList);
    }
    
    [TestMethod]
    public void ShowOk()
    {
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(newStaff);
        controller = new MaintenanceStaffController(service.Object, buildingService.Object, usersService.Object);
        
        var result = controller.Show(1);
        var okResult = result as OkObjectResult;
        MaintenanceStaffDetailModel returnedValue = okResult.Value as MaintenanceStaffDetailModel;
        MaintenanceStaffDetailModel expectedValue = new MaintenanceStaffDetailModel(newStaff);
        
        service.VerifyAll();
        Assert.AreEqual(returnedValue, expectedValue);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(true);
        controller = new MaintenanceStaffController(service.Object, buildingService.Object, usersService.Object);
        
        var result = controller.Delete(UserId);
        var noContentResult = result as NoContentResult;
        
        service.VerifyAll();
        
        Assert.IsNotNull(noContentResult);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(newStaff);
        Building otherBuilding = new Building() {Name = "BuildingName2", Address = "Address2", CommonExpenses = 5, 
            Latitude = 40.001, Longitude = 70.000, Company = new ConstructionCompany(){Id =1, Name ="C1"}, BuildingManager = new Manager(){Id = 1}};
        buildingService.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(otherBuilding);
        service.Setup(logic => logic.Update(It.IsAny<MaintenanceStaff>())).Returns(newStaff);
        controller = new MaintenanceStaffController(service.Object, buildingService.Object, usersService.Object);

        MaintenanceStaffUpdateModel newModel = new MaintenanceStaffUpdateModel() { BuildingId = 1 };
        var result = controller.Update(1, newModel);
        var okResult = result as OkObjectResult;
        MaintenanceStaffDetailModel returnedValue = okResult.Value as MaintenanceStaffDetailModel;
        MaintenanceStaffDetailModel expectedValue = new MaintenanceStaffDetailModel(newStaff);
        
        service.VerifyAll();
        
        Assert.AreEqual(expectedValue, returnedValue);
    }
    
    [TestMethod]
    public void DeleteReturnsFalse()
    {
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(false);
        controller = new MaintenanceStaffController(service.Object, buildingService.Object, usersService.Object);
        
        var result = controller.Delete(UserId);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty(PropertyName);
        
        service.VerifyAll();
        
        Assert.AreEqual(DeleteNotFound, message.GetValue(notFoundResult.Value));
    }
    
    [TestMethod]
    public void CreateOk()
    {
        MaintenanceCreateModel newStaffModel = new MaintenanceCreateModel() { Name = "pepe", LastName = "rodriguez", Password = "pepe", Email = "pepe@gmail.com", AssociatedBuildingId = 1};
        usersService.Setup(logic => logic.ValidateEmail(It.IsAny<string>()));
        service.Setup(logic => logic.Create(It.IsAny<MaintenanceStaff>())).Returns(newStaff);
        controller = new MaintenanceStaffController(service.Object, buildingService.Object, usersService.Object);
        
        var result = controller.Create(newStaffModel);
        var okResult = result as OkObjectResult;
        MaintenanceStaffDetailModel returnedValue = okResult.Value as MaintenanceStaffDetailModel;
        MaintenanceStaffDetailModel expectedValue = new MaintenanceStaffDetailModel(newStaff);
        
        service.VerifyAll();
        usersService.VerifyAll();
        Assert.AreEqual(returnedValue, expectedValue);
    }
}
using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;
using Moq;

namespace Tests.ControllersTests;

[TestClass]
public class MaintenanceControllerTests
{
    private Mock<IMaintenanceLogic> service;
    private const int UserId = 1;
    private MaintenanceStaff newStaff;
    private MaintenanceStaffController controller;
    private const string PropertyName = "Message";
    
    [TestInitialize]
    public void Initialize()
    {
        
        service = new Mock<IMaintenanceLogic>();
        newStaff = new MaintenanceStaff() { Id = UserId, Name = "pepe", LastName = "rodriguez", Password = "pepe", Email = "pepe@gmail.com", AssociatedBuilding = new Building(){Id =1}};
    }
    
    [TestMethod]
    public void IndexOk()
    {
        List<MaintenanceStaff> staff = new List<MaintenanceStaff>() { newStaff };
        service.Setup(logic => logic.GetAll()).Returns(staff);
        controller = new MaintenanceStaffController(service.Object);
        
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
        controller = new MaintenanceStaffController(service.Object);
        
        var result = controller.Show(1);
        var okResult = result as OkObjectResult;
        MaintenanceStaffDetailModel returnedValue = okResult.Value as MaintenanceStaffDetailModel;
        MaintenanceStaffDetailModel expectedValue = new MaintenanceStaffDetailModel(newStaff);
        
        service.VerifyAll();
        Assert.AreEqual(returnedValue, expectedValue);
    }
    
    [TestMethod]
    public void ShowCatchInvalidLogicException()
    {
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Throws(new InvalidStaffLogicException());
        controller = new MaintenanceStaffController(service.Object);
        
        var result = controller.Show(1);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty(PropertyName);
        
        service.VerifyAll();
        Assert.AreEqual("No MaintenanceStaff was found with that ID.", message.GetValue(notFoundResult.Value));
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(true);
        controller = new MaintenanceStaffController(service.Object);
        
        var result = controller.Delete(UserId);
        var noContentResult = result as NoContentResult;
        
        service.VerifyAll();
        
        Assert.IsNotNull(noContentResult);
    }
    
    [TestMethod]
    public void DeleteReturnsFalse()
    {
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(false);
        controller = new MaintenanceStaffController(service.Object);
        
        var result = controller.Delete(UserId);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty(PropertyName);
        
        service.VerifyAll();
        
        Assert.AreEqual("There is no maintenance staff for that Id", message.GetValue(notFoundResult.Value));
    }
    
    [TestMethod]
    public void CreateOk()
    {
        MaintenanceCreateModel newStaffModel = new MaintenanceCreateModel() { Name = "pepe", LastName = "rodriguez", Password = "pepe", Email = "pepe@gmail.com", AssociatedBuilding = new Building(){Id =1}};
        service.Setup(logic => logic.Create(It.IsAny<MaintenanceStaff>())).Returns(newStaff);
        controller = new MaintenanceStaffController(service.Object);
        
        var result = controller.Create(newStaffModel);
        var okResult = result as OkObjectResult;
        MaintenanceStaffDetailModel returnedValue = okResult.Value as MaintenanceStaffDetailModel;
        MaintenanceStaffDetailModel expectedValue = new MaintenanceStaffDetailModel(newStaff);
        
        service.VerifyAll();
        Assert.AreEqual(returnedValue, expectedValue);
    }
    
    [TestMethod]
    public void CreateBadRequestForEmptyBuilding()
    {
        MaintenanceCreateModel newStaffModel = new MaintenanceCreateModel() { Name = "pepe", LastName = "rodriguez", Password = "pepe", Email = "pepe@gmail.com", AssociatedBuilding = null};
        service.Setup(logic => logic.Create(It.IsAny<MaintenanceStaff>())).Returns(newStaff);
        controller = new MaintenanceStaffController(service.Object);
        
        var result = controller.Create(newStaffModel);
        var badResult = result as BadRequestObjectResult;
        var message = badResult.Value.GetType().GetProperty(PropertyName);
        
        Assert.AreEqual("Ensure not to input empty or null data.", message.GetValue(badResult.Value));
    }
    
    [TestMethod]
    public void CreateBadRequestForEmptyUserAttributes()
    {
        MaintenanceCreateModel newStaffModel = new MaintenanceCreateModel() { Name = "pepe", LastName = "", Password = "pepe", Email = "pepe@gmail.com", AssociatedBuilding = new Building(){Id = 1}};
        service.Setup(logic => logic.Create(It.IsAny<MaintenanceStaff>())).Returns(newStaff);
        controller = new MaintenanceStaffController(service.Object);
        
        var result = controller.Create(newStaffModel);
        var badResult = result as BadRequestObjectResult;
        var message = badResult.Value.GetType().GetProperty(PropertyName);
        
        Assert.AreEqual("Ensure not to input empty or null data.", message.GetValue(badResult.Value));
    }
    
    [TestMethod]
    public void CreateCatchInvalidStaffLogicException()
    {
        MaintenanceCreateModel newStaffModel = new MaintenanceCreateModel() { Name = "pepe", LastName = "pep", Password = "pepe", Email = "pepe@gmail.com", AssociatedBuilding = new Building(){Id = 1}};
        service.Setup(logic => logic.Create(It.IsAny<MaintenanceStaff>())).Throws(new InvalidStaffLogicException());
        controller = new MaintenanceStaffController(service.Object);
        
        var result = controller.Create(newStaffModel);
        var badResult = result as BadRequestObjectResult;
        var message = badResult.Value.GetType().GetProperty(PropertyName);
        
        Assert.AreEqual("There is already maintenance staff with that email, please enter another one.", message.GetValue(badResult.Value));
    }
}
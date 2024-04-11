using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;

namespace Tests.ControllersTests;

[TestClass]
public class MaintenanceControllerTests
{
    private Mock<IMaintenanceLogic> service;
    private const int UserId = 1;
    private MaintenanceStaff newStaff;
    private MaintenanceStaffController controller;
    
    [TestInitialize]
    public void Initialize()
    {
        
        service = new Mock<IMaintenanceLogic>();
        newStaff = new MaintenanceStaff() { Id = UserId };
    }
    
    [TestMethod]
    public void IndexOk()
    {
        List<MaintenanceStaff> staff = new List<MaintenanceStaff>() { newStaff };
        service.Setup(logic => logic.GetAll()).Returns(staff);
        controller = new MaintenanceStaffController(service.Object);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<MaintenanceStaff> returnedValue = okResult.Value as List<MaintenanceStaff>;
        List<MaintenanceStaff> expectedList = new List<MaintenanceStaff>() { new MaintenanceStaff() { Id = UserId } };

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
        MaintenanceStaff returnedValue = okResult.Value as MaintenanceStaff;
        MaintenanceStaff expectedValue = new MaintenanceStaff() { Id = UserId };
        
        service.VerifyAll();
        Assert.AreEqual(returnedValue, expectedValue);
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
    public void CreateOk()
    {
        service.Setup(logic => logic.Create(It.IsAny<MaintenanceStaff>())).Returns(newStaff);
        controller = new MaintenanceStaffController(service.Object);
        
        var result = controller.Create(newStaff);
        var okResult = result as OkObjectResult;
        MaintenanceStaff returnedValue = okResult.Value as MaintenanceStaff;
        MaintenanceStaff expectedValue = new MaintenanceStaff() { Id = UserId };
        
        service.VerifyAll();
        Assert.AreEqual(returnedValue, expectedValue);
    }
}
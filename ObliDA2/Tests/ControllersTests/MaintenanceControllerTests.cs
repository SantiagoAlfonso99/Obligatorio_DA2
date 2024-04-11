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
    [TestMethod]
    public void IndexOk()
    {
        Mock<IMaintenanceLogic> service = new Mock<IMaintenanceLogic>();
        List<MaintenanceStaff> staff = new List<MaintenanceStaff>() { new MaintenanceStaff() { Id = 1 } };
        service.Setup(logic => logic.GetAll()).Returns(staff);
        MaintenanceStaffController controller = new MaintenanceStaffController(service.Object);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<MaintenanceStaff> returnedValue = okResult.Value as List<MaintenanceStaff>;
        List<MaintenanceStaff> expectedList = new List<MaintenanceStaff>() { new MaintenanceStaff() { Id = 1 } };

        service.VerifyAll();
        CollectionAssert.AreEqual(returnedValue, expectedList);
    }
    
    [TestMethod]
    public void ShowOk()
    {
        Mock<IMaintenanceLogic> service = new Mock<IMaintenanceLogic>();
        MaintenanceStaff newIndividual = new MaintenanceStaff() { Id = 1 };
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(newIndividual);
        MaintenanceStaffController controller = new MaintenanceStaffController(service.Object);
        
        var result = controller.Show(1);
        var okResult = result as OkObjectResult;
        MaintenanceStaff returnedValue = okResult.Value as MaintenanceStaff;
        MaintenanceStaff expectedValue = new MaintenanceStaff() { Id = 1 };
        
        service.VerifyAll();
        Assert.AreEqual(returnedValue, expectedValue);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Mock<IMaintenanceLogic> service = new Mock<IMaintenanceLogic>();
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(true);
        MaintenanceStaffController controller = new MaintenanceStaffController(service.Object);
        
        var result = controller.Delete(1);
        var noContentResult = result as NoContentResult;
        
        service.VerifyAll();
        
        Assert.IsNotNull(noContentResult);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        Mock<IMaintenanceLogic> service = new Mock<IMaintenanceLogic>();
        MaintenanceStaff newIndividual = new MaintenanceStaff() { Id = 1 };
        service.Setup(logic => logic.Create(It.IsAny<MaintenanceStaff>())).Returns(newIndividual);
        MaintenanceStaffController controller = new MaintenanceStaffController(service.Object);
        
        var result = controller.Create(newIndividual);
        var okResult = result as OkObjectResult;
        MaintenanceStaff returnedValue = okResult.Value as MaintenanceStaff;
        MaintenanceStaff expectedValue = new MaintenanceStaff() { Id = 1 };
        
        service.VerifyAll();
        Assert.AreEqual(returnedValue, expectedValue);
    }
}
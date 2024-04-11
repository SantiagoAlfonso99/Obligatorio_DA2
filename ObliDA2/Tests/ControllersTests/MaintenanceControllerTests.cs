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
}
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;

namespace Tests.ControllersTests;

[TestClass]
public class AdminControllerTest
{
    [TestMethod]
    public void IndexOkTest()
    {
        Admin newAdmin = new Admin()
            { Id = 1, Name = "pepe", LastName = "perez", Email = "pepe@gmail.com", Password = "pepe" };
        List<Admin> admins = new List<Admin>() { newAdmin };
        Mock<IAdminLogic> adminLogicMock = new Mock<IAdminLogic>();
        adminLogicMock.Setup(r => r.GetAll()).Returns(admins);
        AdminController controller = new AdminController(adminLogicMock.Object);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<Admin> returnedAdmins = okResult.Value as List<Admin>;

        adminLogicMock.VerifyAll();
        CollectionAssert.AreEqual(
            admins,
            returnedAdmins
        );
    }
}
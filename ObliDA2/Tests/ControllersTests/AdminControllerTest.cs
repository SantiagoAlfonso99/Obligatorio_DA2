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
    
    [TestMethod]
    public void ShowOkTest()
    {
        Admin newAdmin = new Admin()
            { Id = 1, Name = "pepe", LastName = "perez", Email = "pepe@gmail.com", Password = "pepe" };
        Mock<IAdminLogic> adminLogicMock = new Mock<IAdminLogic>();
        adminLogicMock.Setup(r => r.GetById(1)).Returns(newAdmin);
        AdminController adminController = new AdminController(adminLogicMock.Object);
        
        var result = adminController.Show(1);
        var okResult = result as OkObjectResult;
        var returnedAdmin = okResult.Value as Admin;
        
        adminLogicMock.VerifyAll();
        
        Assert.AreEqual(newAdmin, returnedAdmin);
    }
}
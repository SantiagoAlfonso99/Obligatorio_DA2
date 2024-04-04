using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;

namespace Tests.ControllersTests;

[TestClass]
public class AdminControllerTest
{
    private Admin newAdmin;
    private AdminCreateModel newAdminModel;
    private List<Admin> admins;
    private Mock<IAdminLogic> adminLogicMock;
    private AdminController controller;
    private const int UserId = 1;
    private const string ValidName = "pepe";
    private const string ValidPassword = "a";
    private const string ValidLastName = "perez";
    private const string ValidEmail = "pepe@gmail.com";
    
    [TestInitialize]
    public void Initialize()
    {
        newAdminModel = new AdminCreateModel()
            {Name = ValidName, LastName = ValidLastName, Email = ValidEmail, Password = ValidPassword };
        newAdmin = new Admin()
            { Id = UserId, Name = ValidName, LastName = ValidLastName, Email = ValidEmail, Password = ValidPassword };
        admins = new List<Admin>() { newAdmin };
        adminLogicMock = new Mock<IAdminLogic>();
    }
    
    [TestMethod]
    public void IndexOkTest()
    {
        adminLogicMock.Setup(r => r.GetAll()).Returns(admins);
        AdminController controller = new AdminController(adminLogicMock.Object);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<AdminDetailModel> returnedAdmins = okResult.Value as List<AdminDetailModel>;
        List<AdminDetailModel> expectedModels = admins.Select(admin => new AdminDetailModel(admin)).ToList();
        
        adminLogicMock.VerifyAll();
        CollectionAssert.AreEqual(
            expectedModels,
            returnedAdmins
        );
    }
    
    [TestMethod]
    public void ShowOkTest()
    {
        adminLogicMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(newAdmin);
        AdminController adminController = new AdminController(adminLogicMock.Object);
        
        var result = adminController.Show(UserId);
        var okResult = result as OkObjectResult;
        var returnedAdmin = okResult.Value as AdminDetailModel;
        var expectedAdmin = new AdminDetailModel(newAdmin);
        
        adminLogicMock.VerifyAll();
        
        Assert.AreEqual(expectedAdmin, returnedAdmin);
    }
    
    [TestMethod]
    public void CreateOkTest()
    {
        adminLogicMock.Setup(r => r.Create(It.IsAny<Admin>())).Returns(newAdmin);
        AdminController adminController = new AdminController(adminLogicMock.Object);
        
        var result = adminController.Create(newAdminModel);
        var okResult = result as OkObjectResult;
        var returnedAdmin = okResult.Value as AdminDetailModel;
        var adminExpected = new AdminDetailModel(newAdmin);
        
        adminLogicMock.VerifyAll();
        
        Assert.AreEqual(adminExpected, returnedAdmin);
    }
    
    [TestMethod]
    public void UpdateOkTest()
    {
        Admin newAttributes = new Admin() {Name = "OtherName", LastName = "otherLastName", Password = "otherPassword" };
        adminLogicMock.Setup(r => r.Update(It.IsAny<int>() ,It.IsAny<Admin>())).Returns(newAdmin);
        AdminController adminController = new AdminController(adminLogicMock.Object);
        
        var result = adminController.Update(1, newAttributes);
        var okResult = result as OkObjectResult;
        var returnedAdmin = okResult.Value as AdminDetailModel;
        var adminExpected = new AdminDetailModel(newAdmin);
        
        adminLogicMock.VerifyAll();
        
        Assert.AreEqual(adminExpected, returnedAdmin);
    }
    
    [TestMethod]
    public void DeleteOkTest()
    {
        adminLogicMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(true);
        AdminController adminController = new AdminController(adminLogicMock.Object);
        
        var result = adminController.Delete(1);
        var noContentResult = result as NoContentResult;
        
        adminLogicMock.VerifyAll();
        
        Assert.IsNotNull(noContentResult);
    }
}
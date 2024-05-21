using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;

namespace Tests.ControllersTests;

[TestClass]
public class CompanyControllerTests
{
    private Mock<IUsersLogic> userService;
    private Mock<ICompanyAdminLogic> adminService;
    private ConstructionCompanyController controller;
    private const string Message = "An administrator of companies can only create a single construction company.";
    private CompanyCreateModel newCompany;
    private ConstructionCompany returnedCompany;
    private CompanyAdmin admin;
    
    [TestInitialize]
    public void Initialize()
    {
        newCompany = new CompanyCreateModel() { Name = "Company1"};
        returnedCompany = new ConstructionCompany() { Name = "Company1", Id = 1 };
        admin = new CompanyAdmin() { Name = "name", Email = "email@gmail.com", Password = "pepe", Id = 1, Company = returnedCompany};

        userService = new Mock<IUsersLogic>();
        adminService = new Mock<ICompanyAdminLogic>();
        controller = new ConstructionCompanyController(adminService.Object, userService.Object);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        admin.Company = null;
        userService.Setup(logic => logic.GetCurrentUser(It.IsAny<Guid?>())).Returns(admin);
        adminService.Setup(service => service.CreateCompany(It.IsAny<ConstructionCompany>(), It.IsAny<CompanyAdmin>()))
            .Returns(returnedCompany);
        
        var result = controller.Create(newCompany);
        var okResult = result as OkObjectResult;
        CompanyConstructionDetailModel expectedCompany = new CompanyConstructionDetailModel(returnedCompany);
        CompanyConstructionDetailModel company = okResult.Value as CompanyConstructionDetailModel;
        
        userService.VerifyAll();
        adminService.VerifyAll();
        Assert.AreEqual(expectedCompany.Name, company.Name);
    }
    
    [TestMethod]
    public void CreateReturnsBadRequest()
    {
        userService.Setup(logic => logic.GetCurrentUser(It.IsAny<Guid?>())).Returns(admin);
        
        var result = controller.Create(newCompany);
        var badResult = result as BadRequestObjectResult;
        var message = badResult.Value.GetType().GetProperty("Message");
        
        userService.VerifyAll();
        Assert.AreEqual(Message, message.GetValue(badResult.Value));
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        ConstructionCompany returnedCompany = new ConstructionCompany() { Name = "Company1", Id = 1 };
        
        adminService.Setup(service => service.GetById(It.IsAny<int>()))
            .Returns(returnedCompany);
        adminService.Setup(service => service.UpdateCompany(It.IsAny<ConstructionCompany>()))
            .Returns(returnedCompany);

        CompanyCreateModel newModel = new CompanyCreateModel() { Name = "Company2" };
        var result = controller.Update(1, newModel);
        var okResult = result as OkObjectResult;
        CompanyConstructionDetailModel expectedCompany = new CompanyConstructionDetailModel(returnedCompany);
        CompanyConstructionDetailModel company = okResult.Value as CompanyConstructionDetailModel;
        
        userService.VerifyAll();
        adminService.VerifyAll();
        Assert.AreEqual(expectedCompany.Name, company.Name);
    }
}
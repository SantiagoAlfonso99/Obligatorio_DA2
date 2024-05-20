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
    [TestMethod]
    public void CreateOk()
    {
        Mock<IUsersLogic> userService = new Mock<IUsersLogic>();
        Mock<ICompanyAdminLogic> adminService = new Mock<ICompanyAdminLogic>();
        ConstructionCompanyController controller = new ConstructionCompanyController(adminService.Object, userService.Object);
        CompanyCreateModel newCompany = new CompanyCreateModel() { Name = "Company1"};
        CompanyAdmin admin = new CompanyAdmin() { Name = "name", Email = "email@gmail.com", Password = "pepe", Id = 1 };
        ConstructionCompany returnedCompany = new ConstructionCompany() { Name = "Company1", Id = 1 };
        
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
        Mock<IUsersLogic> userService = new Mock<IUsersLogic>();
        Mock<ICompanyAdminLogic> adminService = new Mock<ICompanyAdminLogic>();
        ConstructionCompanyController controller = new ConstructionCompanyController(adminService.Object, userService.Object);
        CompanyCreateModel newCompany = new CompanyCreateModel() { Name = "Company1"};
        ConstructionCompany returnedCompany = new ConstructionCompany() { Name = "Company1", Id = 1 };
        CompanyAdmin admin = new CompanyAdmin() { Name = "name", Email = "email@gmail.com", Password = "pepe", Id = 1, Company = returnedCompany};
        
        userService.Setup(logic => logic.GetCurrentUser(It.IsAny<Guid?>())).Returns(admin);
        
        
        var result = controller.Create(newCompany);
        var badResult = result as BadRequestObjectResult;
        var message = badResult.Value.GetType().GetProperty("Message");
        
        userService.VerifyAll();
        Assert.AreEqual("An administrator of companies can only create a single construction company.", message.GetValue(badResult.Value));
    }
    
    public void UpdateOk()
    {
        Mock<IUsersLogic> userService = new Mock<IUsersLogic>();
        Mock<ICompanyAdminLogic> adminService = new Mock<ICompanyAdminLogic>();
        ConstructionCompanyController controller = new ConstructionCompanyController(adminService.Object, userService.Object);
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
using IBusinessLogic;
using Domain.Models;
using BusinessLogic.Services;
using Moq;

using BusinessLogic.IRepository;

namespace Tests.ServicesTests;

[TestClass]
public class CompanyAdminLogicTests
{
    [TestMethod]
    public void CreateOk()
    {
        Mock<ICompanyAdminRepository> companyAdminsRepo = new Mock<ICompanyAdminRepository>();
        CompanyAdminLogic service = new CompanyAdminLogic(companyAdminsRepo.Object);
        companyAdminsRepo.Setup(repository => repository.Create(It.IsAny<CompanyAdmin>()));
        CompanyAdmin companyAdmin = new CompanyAdmin() {Email = "perez@gmail.com", Password = "password123", Name = "Matias", Id = 1};

        CompanyAdmin returnedCompanyAdmin = service.Create(companyAdmin);
        
        companyAdminsRepo.VerifyAll();
        Assert.AreEqual(companyAdmin, returnedCompanyAdmin);
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        Mock<ICompanyAdminRepository> companyAdminsRepo = new Mock<ICompanyAdminRepository>();
        CompanyAdminLogic service = new CompanyAdminLogic(companyAdminsRepo.Object);
        companyAdminsRepo.Setup(repository => repository.GetAll()).Returns(new List<CompanyAdmin>(){new CompanyAdmin() {Email = "perez@gmail.com", Password = "password123", Name = "Matias", Id = 1}});
        List<CompanyAdmin> expectedAdmins = new List<CompanyAdmin>(){new CompanyAdmin() {Email = "perez@gmail.com", Password = "password123", Name = "Matias", Id = 1}};
        
        List<CompanyAdmin> returnedCompanyAdmins = service.GetAll();
        
        companyAdminsRepo.VerifyAll();
        CollectionAssert.AreEqual(expectedAdmins, returnedCompanyAdmins);
    }
}
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
        Mock<IConstructionCompanyRepository> companyRepo = new Mock<IConstructionCompanyRepository>();
        CompanyAdminLogic service = new CompanyAdminLogic(companyAdminsRepo.Object, companyRepo.Object);
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
        Mock<IConstructionCompanyRepository> companyRepo = new Mock<IConstructionCompanyRepository>();
        CompanyAdminLogic service = new CompanyAdminLogic(companyAdminsRepo.Object, companyRepo.Object);
        companyAdminsRepo.Setup(repository => repository.GetAll()).Returns(new List<CompanyAdmin>(){new CompanyAdmin() {Email = "perez@gmail.com", Password = "password123", Name = "Matias", Id = 1}});
        List<CompanyAdmin> expectedAdmins = new List<CompanyAdmin>(){new CompanyAdmin() {Email = "perez@gmail.com", Password = "password123", Name = "Matias", Id = 1}};
        
        List<CompanyAdmin> returnedCompanyAdmins = service.GetAll();
        
        companyAdminsRepo.VerifyAll();
        CollectionAssert.AreEqual(expectedAdmins, returnedCompanyAdmins);
    }
    
    [TestMethod]
    public void CreateCompanyOk()
    {
        Mock<ICompanyAdminRepository> companyAdminsRepo = new Mock<ICompanyAdminRepository>();
        Mock<IConstructionCompanyRepository> companyRepo = new Mock<IConstructionCompanyRepository>();
        CompanyAdminLogic service = new CompanyAdminLogic(companyAdminsRepo.Object, companyRepo.Object);
        companyRepo.Setup(repository => repository.Create(It.IsAny<ConstructionCompany>()));
        ConstructionCompany newCompany = new ConstructionCompany() { Name = "Company1", Id =1 };
        ConstructionCompany expectedCompany = new ConstructionCompany() { Name = "Company1", Id =1 };
        
        ConstructionCompany returnedCompany = service.CreateCompany(newCompany);
        
        companyAdminsRepo.VerifyAll();
        companyRepo.VerifyAll();
        Assert.AreEqual(expectedCompany, returnedCompany);
    }
    
    [TestMethod]
    public void GetCompanyByIdOk()
    {
        Mock<ICompanyAdminRepository> companyAdminsRepo = new Mock<ICompanyAdminRepository>();
        Mock<IConstructionCompanyRepository> companyRepo = new Mock<IConstructionCompanyRepository>();
        CompanyAdminLogic service = new CompanyAdminLogic(companyAdminsRepo.Object, companyRepo.Object);
        ConstructionCompany newCompany = new ConstructionCompany() { Name = "Company1", Id =1 };
        companyRepo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newCompany);
        ConstructionCompany expectedCompany = new ConstructionCompany() { Name = "Company1", Id =1 };
        
        ConstructionCompany returnedCompany = service.GetById(1);
        
        companyAdminsRepo.VerifyAll();
        companyRepo.VerifyAll();
        Assert.AreEqual(expectedCompany, returnedCompany);
    }
}
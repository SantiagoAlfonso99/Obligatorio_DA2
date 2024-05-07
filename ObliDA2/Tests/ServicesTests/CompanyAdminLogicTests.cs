using IBusinessLogic;
using Domain.Models;
using BusinessLogic.Services;
using Moq;
using Domain.Exceptions;

using BusinessLogic.IRepository;

namespace Tests.ServicesTests;

[TestClass]
public class CompanyAdminLogicTests
{
    private CompanyAdminLogic service;
    private Mock<ICompanyAdminRepository> companyAdminsRepo;
    private Mock<IConstructionCompanyRepository> companyRepo;
    
    [TestInitialize]
    public void Initialize()
    {
        companyAdminsRepo = new Mock<ICompanyAdminRepository>();
        companyRepo = new Mock<IConstructionCompanyRepository>();
        service = new CompanyAdminLogic(companyAdminsRepo.Object, companyRepo.Object);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        companyAdminsRepo.Setup(repository => repository.Create(It.IsAny<CompanyAdmin>()));
        CompanyAdmin companyAdmin = new CompanyAdmin() {Email = "perez@gmail.com", Password = "password123", Name = "Matias", Id = 1};

        CompanyAdmin returnedCompanyAdmin = service.Create(companyAdmin);
        
        companyAdminsRepo.VerifyAll();
        Assert.AreEqual(companyAdmin, returnedCompanyAdmin);
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        companyAdminsRepo.Setup(repository => repository.GetAll()).Returns(new List<CompanyAdmin>(){new CompanyAdmin() {Email = "perez@gmail.com", Password = "password123", Name = "Matias", Id = 1}});
        List<CompanyAdmin> expectedAdmins = new List<CompanyAdmin>(){new CompanyAdmin() {Email = "perez@gmail.com", Password = "password123", Name = "Matias", Id = 1}};
        
        List<CompanyAdmin> returnedCompanyAdmins = service.GetAll();
        
        companyAdminsRepo.VerifyAll();
        CollectionAssert.AreEqual(expectedAdmins, returnedCompanyAdmins);
    }
    
    [TestMethod]
    public void CreateCompanyOk()
    {
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
        ConstructionCompany newCompany = new ConstructionCompany() { Name = "Company1", Id =1 };
        companyRepo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newCompany);
        ConstructionCompany expectedCompany = new ConstructionCompany() { Name = "Company1", Id =1 };
        
        ConstructionCompany returnedCompany = service.GetById(1);
        
        companyAdminsRepo.VerifyAll();
        companyRepo.VerifyAll();
        Assert.AreEqual(expectedCompany, returnedCompany);
    }
    
    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void GetCompanyByIdThrowsException()
    {
        ConstructionCompany newCompany = null;
        companyRepo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newCompany);
        ConstructionCompany expectedCompany = new ConstructionCompany() { Name = "Company1", Id =1 };
        
        ConstructionCompany returnedCompany = service.GetById(1);
        
        companyAdminsRepo.VerifyAll();
        companyRepo.VerifyAll();
    }
}
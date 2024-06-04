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
        List<ConstructionCompany> companies = new List<ConstructionCompany>()
        {
            new ConstructionCompany() { Id = 1, Name = "c1" },
            new ConstructionCompany() { Id = 2, Name = "c2" },
        };
        ConstructionCompany newCompany = new ConstructionCompany() { Name = "Company1", Id =3 };
        List<ConstructionCompany> otherCompanies = new List<ConstructionCompany>()
        {
            new ConstructionCompany() { Id = 1, Name = "c1" },
            new ConstructionCompany() { Id = 2, Name = "c2" },
            newCompany
        };
        companyRepo.SetupSequence(repository => repository.GetAll()).Returns(companies).Returns(otherCompanies);
        companyRepo.Setup(repository => repository.Create(It.IsAny<ConstructionCompany>())).Callback<ConstructionCompany>(newCompany => companies.Add(newCompany));
        CompanyAdmin admin = new CompanyAdmin() { Name = "pepe", Email = "pepe@gmail.com", Password = "pepe", Id = 1 };
        ConstructionCompany expectedCompany = new ConstructionCompany() { Name = "Company1", Id =3 };
        
        ConstructionCompany returnedCompany = service.CreateCompany(newCompany, admin);
        
        companyAdminsRepo.VerifyAll();
        companyRepo.VerifyAll();
        Assert.AreEqual(expectedCompany, returnedCompany);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DuplicateEntryException))]
    public void CreateCompanyWithInvalidNameThrowsException()
    {
        List<ConstructionCompany> companies = new List<ConstructionCompany>()
        {
            new ConstructionCompany() { Id = 1, Name = "c1" },
            new ConstructionCompany() { Id = 2, Name = "c2" },
            new ConstructionCompany() { Id = 3, Name = "Company1" },
        };
        ConstructionCompany newCompany = new ConstructionCompany() { Name = "Company1", Id =3 };
        companyRepo.SetupSequence(repository => repository.GetAll()).Returns(companies);
        companyRepo.Setup(repository => repository.Create(It.IsAny<ConstructionCompany>())).Callback<ConstructionCompany>(newCompany => companies.Add(newCompany));
        CompanyAdmin admin = new CompanyAdmin() { Name = "pepe", Email = "pepe@gmail.com", Password = "pepe", Id = 1 };
        ConstructionCompany expectedCompany = new ConstructionCompany() { Name = "Company1", Id =3 };
        
        ConstructionCompany returnedCompany = service.CreateCompany(newCompany, admin);
        
        companyAdminsRepo.VerifyAll();
        companyRepo.VerifyAll();
        Assert.AreEqual(expectedCompany, returnedCompany);
    }
    
    [TestMethod]
    [ExpectedException(typeof(EmptyOrNullException))]
    public void CreateCompanyWithEmptyNameThrowsException()
    {
        ConstructionCompany company = new ConstructionCompany() { Name = "" };
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
    public void UpdateOk()
    {
        ConstructionCompany newCompany = new ConstructionCompany() { Name = "Company1", Id =1 };
        companyRepo.Setup(repository => repository.Update(It.IsAny<ConstructionCompany>()));
        companyRepo.Setup(repository => repository.GetAll()).Returns(new List<ConstructionCompany>());
        ConstructionCompany expectedCompany = new ConstructionCompany() { Name = "Company2", Id =1 };
        
        ConstructionCompany returnedCompany = service.UpdateCompany(newCompany,"Company2");
        
        companyAdminsRepo.VerifyAll();
        Assert.AreEqual(expectedCompany, returnedCompany);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DuplicateEntryException))]
    public void UpdateWithUsedNameThrowsExceptionOk()
    {
        ConstructionCompany newCompany = new ConstructionCompany() { Name = "Company2", Id =1 };
        companyRepo.Setup(repository => repository.Update(It.IsAny<ConstructionCompany>()));
        companyRepo.Setup(repository => repository.GetAll()).Returns(new List<ConstructionCompany>(){new ConstructionCompany() { Name = "Company1", Id =2 }});
        ConstructionCompany expectedCompany = new ConstructionCompany() { Name = "Company1", Id =1 };
        
        ConstructionCompany returnedCompany = service.UpdateCompany(newCompany, "Company1");
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
using System.Data.Common;
using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using BusinessLogic.Services;
using BusinessLogic.IRepository;
using Moq;
namespace Tests;

[TestClass]
public class AdminLogicTests
{
    private const int UserId = 1;
    private const string EmptyString = "";
    private const string ValidName = "pepe";
    private const string ValidPassword = "a";
    private const string InvalidPassword = "";
    private const string EmailWithoutTextAtTheEnd = "pepe@";
    private const string EmailWithoutSymbol = "pepe.com";
    private const string ValidEmail = "pepe@gmail.com";
    private const int ExpectedCount = 1;
    private AdminLogic adminService;
    private Mock<IAdminRepository> adminRepo;
        
    [TestInitialize]
    public void Initialize()
    {
        adminRepo = new Mock<IAdminRepository>(MockBehavior.Strict);
    }
    
    [TestMethod]
    public void AdminCountShouldReturnTheExpectedLength()
    {
        adminRepo.Setup(repo => repo.Count()).Returns(1);
        adminService = new AdminLogic(adminRepo.Object);
        int returnedCount = adminService.Count();
        adminRepo.VerifyAll();
        Assert.AreEqual(ExpectedCount, returnedCount);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void InValidAdminNameThrowsException()
    {
        Admin newAdmin = new Admin() { Email = ValidEmail, Id = UserId, Password = ValidPassword, Name = EmptyString };
        adminRepo.Setup(repo => repo.Add(It.IsAny<Admin>()));
        adminService = new AdminLogic(adminRepo.Object);
        Admin returnedAdmin = adminService.Create(newAdmin);
        adminRepo.VerifyAll();
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void InvalidAdminPasswordThrowsException()
    {
        Admin newAdmin = new Admin() { Email = ValidEmail, Id = UserId, Password = InvalidPassword, Name = ValidName };
        adminRepo.Setup(repo => repo.Add(It.IsAny<Admin>()));
        adminService = new AdminLogic(adminRepo.Object);
        Admin returnedAdmin = adminService.Create(newAdmin);
        adminRepo.VerifyAll();
    }
    
    [TestMethod]
    public void SetAttributesShouldAssignCorrectly()
    {
        Admin newAdmin = new Admin() { Email = ValidEmail, Id = UserId, Password = ValidPassword, Name = ValidName };
        adminRepo.Setup(repo => repo.Add(It.IsAny<Admin>()));
        adminService = new AdminLogic(adminRepo.Object);
        Admin returnedAdmin = adminService.Create(newAdmin);
        adminRepo.VerifyAll();
        Assert.IsTrue(newAdmin.AreEqual(returnedAdmin));
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void EmptyEmailThrowsException()
    {
        Admin newAdmin = new Admin() { Email = EmptyString, Id = UserId, Password = ValidPassword, Name = ValidName };
        adminRepo.Setup(repo => repo.Add(It.IsAny<Admin>()));
        Admin returnedAdmin = adminService.Create(newAdmin);
        adminRepo.VerifyAll();
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void EmailWithoutSymbolThrowsException()
    {
        Admin newAdmin = new Admin() { Email = EmailWithoutSymbol, Id = UserId, Password = ValidPassword, Name = ValidName };
        adminRepo.Setup(repo => repo.Add(It.IsAny<Admin>()));
        Admin returnedAdmin = adminService.Create(newAdmin);
        adminRepo.VerifyAll();
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void EmailWithoutTextAtTheEndThrowsException()
    {
        Admin newAdmin = new Admin() { Email = EmailWithoutTextAtTheEnd, Id = UserId, Password = ValidPassword, Name = ValidName };
        adminRepo.Setup(repo => repo.Add(It.IsAny<Admin>()));
        Admin returnedAdmin = adminService.Create(newAdmin);
        adminRepo.VerifyAll();
    }
    
    [TestMethod]
    public void GetByIdShouldReturnsTheExpectedAdmin()
    {
        Admin newAdmin = new Admin() { Email = ValidEmail, Id = UserId, Password = ValidPassword, Name = ValidName };
        adminRepo.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(true);
        adminRepo.Setup(repo => repo.Get(It.IsAny<int>())).Returns(newAdmin);
        adminService = new AdminLogic(adminRepo.Object);
        Admin returnedAdmin = adminService.GetById(1);
        adminRepo.VerifyAll();
        Assert.IsTrue(newAdmin.AreEqual(returnedAdmin));
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidAdminException))]
    public void GetByIdFunctionShouldThrowExceptionForNonExistentId()
    {
        Admin newAdmin = new Admin() { Email = ValidEmail, Id = UserId, Password = ValidPassword, Name = ValidName };
        adminRepo.Setup(repo => repo.Exists(It.IsAny<int>())).Returns(false);
        adminService = new AdminLogic(adminRepo.Object);
        Admin returnedAdmin = adminService.GetById(2);
        adminRepo.VerifyAll();
    }
}
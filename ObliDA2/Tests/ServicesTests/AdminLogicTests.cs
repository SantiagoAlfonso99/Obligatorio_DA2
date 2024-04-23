using System.Data.Common;
using Domain.Exceptions;
using Domain.Models;
using BusinessLogic.Services;
using BusinessLogic.IRepository;
using Moq;
namespace Tests.ServicesTests;

[TestClass]
public class AdminLogicTests
{
    private const int UserId = 1;
    private const string EmptyString = "";
    private const string ValidName = "pepe";
    private const string ValidPassword = "a";
    private const string ValidLastName = "perez";
    private const string InvalidPassword = "";
    private const string EmailWithoutTextAtTheEnd = "pepe@";
    private const string EmailWithoutSymbol = "pepe.com";
    private const string ValidEmail = "pepe@gmail.com";
    private const int ExpectedCount = 1;
    private const string OtherName = "otherName";
    private const string OtherPassword = "otherPassword";
    private const string OtherLastName = "otherLastName";
    private AdminLogic adminService;
    private Mock<IAdminRepository> adminRepo;
        
    [TestInitialize]
    public void Initialize()
    {
        adminRepo = new Mock<IAdminRepository>(MockBehavior.Strict);
    }
    
    [TestMethod]
    public void GetAllAdmins()
    {
        List<Admin> expectedList = new List<Admin>() { new Admin() { Id = UserId } };
        adminRepo.Setup(repo => repo.GetAll()).Returns(expectedList);
        adminService = new AdminLogic(adminRepo.Object);
        
        List<Admin> returnedList = adminService.GetAll();
        adminRepo.VerifyAll();
        CollectionAssert.AreEqual(expectedList, returnedList);
    }
    
    [TestMethod]
    [ExpectedException(typeof(EmptyOrNullException))]
    public void InValidAdminNameThrowsException()
    {
        Admin newAdmin = new Admin() { Email = ValidEmail, Id = UserId, Password = ValidPassword, Name = EmptyString, LastName = ValidLastName };
        adminRepo.Setup(repo => repo.Add(It.IsAny<Admin>()));
        adminService = new AdminLogic(adminRepo.Object);
        Admin returnedAdmin = adminService.Create(newAdmin);
        adminRepo.VerifyAll();
    }
    
    [TestMethod]
    [ExpectedException(typeof(EmptyOrNullException))]
    public void InvalidAdminPasswordThrowsException()
    {
        Admin newAdmin = new Admin() { Email = ValidEmail, Id = UserId, Password = InvalidPassword, Name = ValidName, LastName = ValidLastName };
        adminRepo.Setup(repo => repo.Add(It.IsAny<Admin>()));
        adminService = new AdminLogic(adminRepo.Object);
        Admin returnedAdmin = adminService.Create(newAdmin);
        adminRepo.VerifyAll();
    }
    
    [TestMethod]
    public void SetAttributesShouldAssignCorrectly()
    {
        Admin newAdmin = new Admin() { Email = ValidEmail, Id = UserId, Password = ValidPassword, Name = ValidName, LastName = ValidLastName };
        adminRepo.Setup(repo => repo.Add(It.IsAny<Admin>()));
        adminService = new AdminLogic(adminRepo.Object);
        Admin returnedAdmin = adminService.Create(newAdmin);
        adminRepo.VerifyAll();
        Assert.IsTrue(newAdmin.AreEqual(returnedAdmin));
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void EmptyEmailThrowsException()
    {
        Admin newAdmin = new Admin() { Email = EmptyString, Id = UserId, Password = ValidPassword, Name = ValidName, LastName = ValidLastName };
        adminRepo.Setup(repo => repo.Add(It.IsAny<Admin>()));
        Admin returnedAdmin = adminService.Create(newAdmin);
        adminRepo.VerifyAll();
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void EmailWithoutSymbolThrowsException()
    {
        Admin newAdmin = new Admin() { Email = EmailWithoutSymbol, Id = UserId, Password = ValidPassword, Name = ValidName, LastName = ValidLastName };
        adminRepo.Setup(repo => repo.Add(It.IsAny<Admin>()));
        Admin returnedAdmin = adminService.Create(newAdmin);
        adminRepo.VerifyAll();
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void EmailWithoutTextAtTheEndThrowsException()
    {
        Admin newAdmin = new Admin() { Email = EmailWithoutTextAtTheEnd, Id = UserId, Password = ValidPassword, Name = ValidName, LastName = ValidLastName };
        adminRepo.Setup(repo => repo.Add(It.IsAny<Admin>()));
        Admin returnedAdmin = adminService.Create(newAdmin);
        adminRepo.VerifyAll();
    }
    
    [TestMethod]
    public void GetByIdShouldReturnsTheExpectedAdmin()
    {
        Admin newAdmin = new Admin() { Email = ValidEmail, Id = UserId, Password = ValidPassword, Name = ValidName, LastName = ValidLastName };
        adminRepo.Setup(repo => repo.Get(It.IsAny<int>())).Returns(newAdmin);
        adminService = new AdminLogic(adminRepo.Object);
        Admin returnedAdmin = adminService.GetById(UserId);
        adminRepo.VerifyAll();
        Assert.IsTrue(newAdmin.AreEqual(returnedAdmin));
    }
    
    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void GetByIdFunctionShouldThrowExceptionForNonExistentId()
    {
        Admin newAdmin = null;
        adminRepo.Setup(repo => repo.Get(It.IsAny<int>())).Returns(newAdmin);
        adminService = new AdminLogic(adminRepo.Object);
        Admin returnedAdmin = adminService.GetById(UserId);
        adminRepo.VerifyAll();
    }
    
    [TestMethod]
    public void CorrectAttributeAssignmentForUpdatedAdmin()
    {
        Admin newAdmin = new Admin() { Email = ValidEmail, Id = UserId, Password = ValidPassword, Name = ValidName, LastName = ValidLastName };
        Admin newAttributes = new Admin() { Password = OtherPassword, Name = OtherName, LastName = OtherLastName };
        Admin expectedAdmin = new Admin() { Email = ValidEmail, Id = UserId, Password = OtherPassword, Name = OtherName, LastName = "otherLastName"};
        adminRepo.Setup(repo => repo.Get(It.IsAny<int>())).Returns(newAdmin);
        adminRepo.Setup(repo => repo.Update(It.IsAny<Admin>()));
        adminService = new AdminLogic(adminRepo.Object);
        Admin returnedAdmin = adminService.Update(UserId,newAttributes);
        adminRepo.VerifyAll();
        Assert.IsTrue(returnedAdmin.AreEqual(expectedAdmin));
    }
    
    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void UpdateFunctionShouldThrowExceptionForNonExistentId()
    {
        Admin newAdmin = null; 
        adminRepo.Setup(repo => repo.Get(It.IsAny<int>())).Returns(newAdmin);
        Admin newAttributes = new Admin() { Password = OtherPassword, Name = OtherName, LastName = OtherLastName };
        adminService = new AdminLogic(adminRepo.Object);
        Admin returnedAdmin = adminService.Update(UserId, newAttributes);
        adminRepo.VerifyAll();
    }
    
    [TestMethod]
    public void DeleteFunctionShouldReturnTrueForExistingId()
    {
        Admin newAdmin = new Admin() { Email = ValidEmail, Id = UserId, Password = ValidPassword, Name = ValidName, LastName = ValidLastName };
        adminRepo.Setup(repo => repo.Get(It.IsAny<int>())).Returns(newAdmin);
        adminRepo.Setup(repo => repo.Remove(It.IsAny<Admin>()));
        adminService = new AdminLogic(adminRepo.Object);
        bool successfullyDelete = adminService.Delete(UserId);
        adminRepo.VerifyAll();
        Assert.IsTrue(successfullyDelete);
    }
    
    [TestMethod]
    public void DeleteFunctionReturnsFalseForNonExistentId()
    {
        Admin newAdmin = null;
        adminRepo.Setup(repo => repo.Get(It.IsAny<int>())).Returns(newAdmin);
        adminService = new AdminLogic(adminRepo.Object);
        bool success = adminService.Delete(UserId);
        adminRepo.VerifyAll();
        Assert.IsFalse(success);
    }
    
    [TestMethod]
    [ExpectedException(typeof(EmptyOrNullException))]
    public void EmptyLastNameThrowsException()
    {
        Admin newAdmin = new Admin() { Email = ValidEmail, Id = UserId, Password = ValidPassword, Name = ValidName, LastName = "" };
        adminRepo.Setup(repo => repo.Add(It.IsAny<Admin>()));
        Admin returnedAdmin = adminService.Create(newAdmin);
        adminRepo.VerifyAll();
    }
}
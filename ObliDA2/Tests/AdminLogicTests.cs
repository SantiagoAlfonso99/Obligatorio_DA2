using System.Data.Common;
using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using BusinessLogic.Services;
namespace Tests;

[TestClass]
public class AdminLogicTests
{
    private const int UserId = 1;
    private const string InvalidName = "";
    private const string ValidName = "pepe";
    private const string ValidPassword = "a";
    private const string InvalidPassword = "";
    private AdminLogic adminService;
    
    [TestInitialize]
    public void Initialize()
    {
        adminService = new AdminLogic();
    }
    
    [TestMethod]
    public void ValidAdminCreation()
    {
        adminService.CreateAdmin(UserId, ValidName, ValidPassword, "pepe@gmail.com");
        Assert.AreEqual(UserId,adminService.AdminsCount());
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void InValidAdminNameThrowsException()
    {
        adminService.CreateAdmin(UserId, InvalidName, ValidPassword, "pepe@gmail.com");
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void InvalidAdminPasswordThrowsException()
    {
        adminService.CreateAdmin(UserId, ValidName, InvalidPassword, "pepe@gmail.com");
    }
    
    [TestMethod]
    public void SetAttributesShouldAssignCorrectly()
    {
        adminService.CreateAdmin(UserId, ValidName, ValidPassword, "pepe@gmail.com");
        Admin expectedAdmin = new Admin() { Id = UserId, Name = ValidName, Password = ValidPassword, Email = "pepe@gmail.com" };
        Admin returnedAdmin = adminService.ReturnAdmin(UserId);
        Assert.IsTrue((returnedAdmin.AreEqual(expectedAdmin)));
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void EmptyEmailThrowsException()
    {
        adminService.CreateAdmin(UserId, ValidName, ValidPassword, "");
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void EmailWithoutSymbolThrowsException()
    {
        adminService.CreateAdmin(UserId, ValidName, ValidPassword, "pepe.com");
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void EmailWithoutTextAtTheEndThrowsException()
    {
        adminService.CreateAdmin(UserId, ValidName, ValidPassword, "pepe@");
    }
}
using System.Data.Common;
using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using BusinessLogic.Services;
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
    private AdminLogic adminService;
    
    [TestInitialize]
    public void Initialize()
    {
        adminService = new AdminLogic();
    }
    
    [TestMethod]
    public void ValidAdminCreation()
    {
        adminService.CreateAdmin(UserId, ValidName, ValidPassword, ValidEmail);
        Assert.AreEqual(UserId,adminService.AdminsCount());
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void InValidAdminNameThrowsException()
    {
        adminService.CreateAdmin(UserId, EmptyString, ValidPassword, ValidEmail);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void InvalidAdminPasswordThrowsException()
    {
        adminService.CreateAdmin(UserId, ValidName, InvalidPassword, ValidEmail);
    }
    
    [TestMethod]
    public void SetAttributesShouldAssignCorrectly()
    {
        adminService.CreateAdmin(UserId, ValidName, ValidPassword, ValidEmail);
        Admin expectedAdmin = new Admin() { Id = UserId, Name = ValidName, Password = ValidPassword, Email = "pepe@gmail.com" };
        Admin returnedAdmin = adminService.ReturnAdmin(UserId);
        Assert.IsTrue((returnedAdmin.AreEqual(expectedAdmin)));
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void EmptyEmailThrowsException()
    {
        adminService.CreateAdmin(UserId, ValidName, ValidPassword, EmptyString);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void EmailWithoutSymbolThrowsException()
    {
        adminService.CreateAdmin(UserId, ValidName, ValidPassword, EmailWithoutSymbol);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void EmailWithoutTextAtTheEndThrowsException()
    {
        adminService.CreateAdmin(UserId, ValidName, ValidPassword, EmailWithoutTextAtTheEnd);
    }
}
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
        adminService.CreateAdmin(UserId, ValidName, ValidPassword);
        Assert.AreEqual(UserId,adminService.AdminsCount());
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void InValidAdminNameThrowsException()
    {
        adminService.CreateAdmin(UserId, InvalidName, ValidPassword);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void InvalidAdminPasswordThrowsException()
    {
        adminService.CreateAdmin(UserId, InvalidName, InvalidPassword);
    }
    
    [TestMethod]
    public void SetAttributesShouldAssignCorrectly()
    {
        adminService.CreateAdmin(UserId, ValidName, ValidPassword);
        Admin returnedAdmin = adminService.ReturnAdmin(UserId);
        Assert.IsTrue((returnedAdmin.Password == ValidPassword));
    }
}
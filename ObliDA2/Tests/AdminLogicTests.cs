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
    
    [TestInitialize]
    public void Initialize()
    {
        
    }
    
    [TestMethod]
    public void ValidAdminCreation()
    {
        AdminLogic adminService = new AdminLogic();
        adminService.CreateAdmin(UserId, ValidName, "a");
        Assert.AreEqual(UserId,adminService.AdminsCount());
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void InValidAdminNameThrowsException()
    {
        AdminLogic adminService = new AdminLogic();
        adminService.CreateAdmin(UserId, InvalidName, "a");
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidUserException))]
    public void InvalidAdminPasswordThrowsException()
    {
        AdminLogic adminService = new AdminLogic();
        adminService.CreateAdmin(UserId, InvalidName, "");
    }
    
    [TestMethod]
    public void SetAttributesShouldAssignCorrectly()
    {
        AdminLogic adminService = new AdminLogic();
        adminService.CreateAdmin(UserId, ValidName, "a");
        Admin returnedAdmin = adminService.ReturnAdmin(UserId);
        Assert.IsTrue((returnedAdmin.Password == "a"));
    }
}
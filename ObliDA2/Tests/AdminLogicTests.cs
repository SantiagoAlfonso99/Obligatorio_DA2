using BusinessLogic.Exceptions;
using BusinessLogic.Models;
using BusinessLogic.Services;
namespace Tests;

[TestClass]
public class AdminLogicTests
{
    [TestMethod]
    public void ValidAdminCreation()
    {
        AdminLogic adminService = new AdminLogic();
        adminService.CreateAdmin(1,"pepe2");
        Assert.AreEqual(1,adminService.AdminsCount());
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidAdminException))]
    public void InValidAdminNameThrowsException()
    {
        AdminLogic adminService = new AdminLogic();
        adminService.CreateAdmin(1,"");
    }
}
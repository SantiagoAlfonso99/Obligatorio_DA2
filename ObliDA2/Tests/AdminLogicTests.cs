using BusinessLogic;
namespace Tests;

[TestClass]
public class AdminLogicTests
{
    [TestMethod]
    public void ValidAdminCreation()
    {
        AdminLogic adminService = new AdminLogic();
        adminService.CreateAdmin(1);
        Assert.AreEqual(1,adminService.AdminsCount());
    }
}
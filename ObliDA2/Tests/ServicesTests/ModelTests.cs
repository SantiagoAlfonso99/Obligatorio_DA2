using Domain.Models;
using Domain.Exceptions;

namespace Tests.ServicesTests;

[TestClass]
public class ModelTests
{
    [TestMethod]
    public void EqualsBuildingReturnsFalse()
    {
        Building building = new Building();
        Invitation invitation = new Invitation();
        bool success = building.Equals(invitation);
        Assert.IsFalse(success);
    }
    
    [TestMethod]
    public void EqualsInvitationReturnsFalse()
    {
        Building building = new Building();
        Invitation invitation = new Invitation();
        bool success = invitation.Equals(building);
        Assert.IsFalse(success);
    }
    
    [TestMethod]
    public void EqualsOwnersReturnsFalse()
    {
        Building building = new Building();
        ApartmentOwner owner = new ApartmentOwner();
        bool success = owner.Equals(building);
        Assert.IsFalse(success);
    }
    
    [TestMethod]
    public void EqualsApartmentReturnsFalse()
    {
        Building building = new Building();
        Apartment apartment = new Apartment();
        bool success = apartment.Equals(building);
        Assert.IsFalse(success);
    }
    
    [TestMethod]
    public void EqualsCategoryReturnsFalse()
    {
        Building building = new Building();
        Category category = new Category();
        bool success = category.Equals(building);
        Assert.IsFalse(success);
    }
    
    [TestMethod]
    public void EqualsMaintenanceStaffReturnsFalse()
    {
        Building building = new Building();
        MaintenanceStaff worker = new MaintenanceStaff();
        bool success = worker.Equals(building);
        Assert.IsFalse(success);
    }
    
    [TestMethod]
    public void EqualsAdminReturnsFalse()
    {
        Admin admin = new Admin();
        bool success = admin.Equals(null);
        Assert.IsFalse(success);
    }
}
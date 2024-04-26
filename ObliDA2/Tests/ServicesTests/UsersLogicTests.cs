using Domain.Exceptions;
using BusinessLogic.IRepository;
using Domain.Models;
using IBusinessLogic;
using Domain.Exceptions;
using Moq;
using BusinessLogic.Services;

namespace Tests.ServicesTests;

[TestClass]
public class UsersLogicTests
{
    private Mock<IManagerRepository> managerRepo;
    private Mock<IMaintenanceStaffRepository> staffRepo;
    private Mock<IAdminRepository> adminRepo;
    private List<Manager> managers;
    private List<Admin> admins;
    private List<MaintenanceStaff> personnel;

    
    [TestInitialize]
    public void Initialize()
    {
        managerRepo = new Mock<IManagerRepository>();
        staffRepo = new Mock<IMaintenanceStaffRepository>();
        adminRepo = new Mock<IAdminRepository>();
        managers = new List<Manager>() { new Manager { Email = "pepe@gmail.com" } };
        admins = new List<Admin>() { new Admin { Email = "raul@gmail.com" } };
        personnel = new List<MaintenanceStaff>() { new MaintenanceStaff { Email = "pepito@gmail.com" } };

    }
    
    [TestMethod]
    [ExpectedException(typeof(DuplicateEntryException))]
    public void DuplicatedEmailThrowsException()
    {
        UsersLogic userService = new UsersLogic(staffRepo.Object, adminRepo.Object, managerRepo.Object);
        
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        
        
        userService.ValidateEmail("pepe@gmail.com");
    }
    
    [TestMethod]
    public void ValidEmailShouldNotThrowAnException()
    {
        UsersLogic userService = new UsersLogic(staffRepo.Object, adminRepo.Object, managerRepo.Object);

        bool success = true;
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        
        userService.ValidateEmail("pepe3@gmail.com");
        
        Assert.AreEqual(success, true);
    }
}
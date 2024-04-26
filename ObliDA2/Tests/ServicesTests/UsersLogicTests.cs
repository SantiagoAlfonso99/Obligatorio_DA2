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

    [TestMethod]
    [ExpectedException(typeof(DuplicateEntryException))]
    public void DuplicatedEmailThrowsException()
    {
        Mock<IManagerRepository> managerRepo = new Mock<IManagerRepository>();
        Mock<IMaintenanceStaffRepository> staffRepo = new Mock<IMaintenanceStaffRepository>();
        Mock<IAdminRepository> adminRepo = new Mock<IAdminRepository>();
        UsersLogic userService = new UsersLogic(staffRepo.Object, adminRepo.Object, managerRepo.Object);

        List<Manager> managers = new List<Manager>() { new Manager { Email = "pepe@gmail.com" } };
        List<Admin> admins = new List<Admin>() { new Admin { Email = "raul@gmail.com" } };
        List<MaintenanceStaff> personnel = new List<MaintenanceStaff>() { new MaintenanceStaff { Email = "pepito@gmail.com" } };
        
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        
        
        userService.ValidateEmail("pepe@gmail.com");
    }
}
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
    private Mock<ISessionRepository> sessionRepo;
    private List<Manager> managers;
    private List<Admin> admins;
    private List<MaintenanceStaff> personnel;

    
    [TestInitialize]
    public void Initialize()
    {
        sessionRepo = new Mock<ISessionRepository>();
        managerRepo = new Mock<IManagerRepository>();
        staffRepo = new Mock<IMaintenanceStaffRepository>();
        adminRepo = new Mock<IAdminRepository>();
        managers = new List<Manager>() { new Manager { Email = "pepe@gmail.com",Name = "pepe",Id =1, Password = "pepe123" } };
        admins = new List<Admin>() { new Admin { Email = "raul@gmail.com",Name = "raul", LastName = "Rodriguez",Id =1, Password = "raul123" } };
        personnel = new List<MaintenanceStaff>() { new MaintenanceStaff { Email = "pepito@gmail.com",Name = "pepito", LastName = "Perez",Id =1, Password = "pepito123" } };

    }
    
    [TestMethod]
    [ExpectedException(typeof(DuplicateEntryException))]
    public void DuplicatedEmailThrowsException()
    {
        UsersLogic userService = new UsersLogic(staffRepo.Object, adminRepo.Object, managerRepo.Object, sessionRepo.Object);
        
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        
        
        userService.ValidateEmail("pepe@gmail.com");
        
        managerRepo.VerifyAll();
        adminRepo.VerifyAll();
        staffRepo.VerifyAll();
    }
    
    [TestMethod]
    public void ValidEmailShouldNotThrowAnException()
    {
        UsersLogic userService = new UsersLogic(staffRepo.Object, adminRepo.Object, managerRepo.Object, sessionRepo.Object);

        bool success = true;
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        
        userService.ValidateEmail("pepe3@gmail.com");
        
        managerRepo.VerifyAll();
        adminRepo.VerifyAll();
        staffRepo.VerifyAll();
        Assert.AreEqual(success, true);
    }

    [TestMethod]
    public void FindAdminEmail()
    {
        UsersLogic userService = new UsersLogic(staffRepo.Object, adminRepo.Object, managerRepo.Object, sessionRepo.Object);
        
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        Admin expectedAdmin = new Admin()
            { Email = "raul@gmail.com", Name = "raul", LastName = "Rodriguez", Id = 1, Password = "raul123" };
        Admin returnedAdmin = (Admin)userService.FindUserByEmail("raul@gmail.com");
        
        managerRepo.VerifyAll();
        adminRepo.VerifyAll();
        staffRepo.VerifyAll();
        Assert.IsTrue(expectedAdmin.AreEqual(returnedAdmin));
    }
    
    [TestMethod]
    public void FindManagerEmail()
    {
        UsersLogic userService = new UsersLogic(staffRepo.Object, adminRepo.Object, managerRepo.Object, sessionRepo.Object);
        
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        Manager expectedManager = new Manager()
            { Email = "pepe@gmail.com",Name = "pepe",Id =1, Password = "pepe123" };
        Manager returnedManager = (Manager)userService.FindUserByEmail("pepe@gmail.com");
        
        managerRepo.VerifyAll();
        adminRepo.VerifyAll();
        staffRepo.VerifyAll();
        Assert.AreEqual(expectedManager, returnedManager);
    }
    
    [TestMethod]
    public void FindMaintenanceStaffEmail()
    {
        UsersLogic userService = new UsersLogic(staffRepo.Object, adminRepo.Object, managerRepo.Object, sessionRepo.Object);
        
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        MaintenanceStaff expectedMaintenanceStaff = new MaintenanceStaff()
            { Email = "pepito@gmail.com",Name = "pepito", LastName = "Perez",Id =1, Password = "pepito123" };
        MaintenanceStaff returnedMaintenanceStaff = (MaintenanceStaff)userService.FindUserByEmail("pepito@gmail.com");
        
        managerRepo.VerifyAll();
        adminRepo.VerifyAll();
        staffRepo.VerifyAll();
        Assert.AreEqual(expectedMaintenanceStaff, returnedMaintenanceStaff);
    }
    
    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void FindUserByEmailThrowsException()
    {
        UsersLogic userService = new UsersLogic(staffRepo.Object, adminRepo.Object, managerRepo.Object, sessionRepo.Object);
        
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        MaintenanceStaff returnedMaintenanceStaff = (MaintenanceStaff)userService.FindUserByEmail("noexiste@gmail.com");
        
        managerRepo.VerifyAll();
        adminRepo.VerifyAll();
        staffRepo.VerifyAll();
    }
    
    [TestMethod]
    public void LogInOk()
    {
        UsersLogic userService = new UsersLogic(staffRepo.Object, adminRepo.Object, managerRepo.Object, sessionRepo.Object);
        
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        MaintenanceStaff expectedMaintenanceStaff = new MaintenanceStaff()
            { Email = "pepito@gmail.com",Name = "pepito", LastName = "Perez",Id =1, Password = "pepito123" };

        
        MaintenanceStaff returnedMaintenanceStaff = (MaintenanceStaff)userService.LogIn("pepito@gmail.com", "pepito123");
        
        managerRepo.VerifyAll();
        adminRepo.VerifyAll();
        staffRepo.VerifyAll();
        Assert.AreEqual(expectedMaintenanceStaff, returnedMaintenanceStaff);
    }
    
    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void LogInWithInvalidPasswordThrowsException()
    {
        UsersLogic userService = new UsersLogic(staffRepo.Object, adminRepo.Object, managerRepo.Object, sessionRepo.Object);
        
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        MaintenanceStaff expectedMaintenanceStaff = new MaintenanceStaff()
            { Email = "pepito@gmail.com",Name = "pepito", LastName = "Perez",Id =1, Password = "pepito123" };

        
        MaintenanceStaff returnedMaintenanceStaff = (MaintenanceStaff)userService.LogIn("pepito@gmail.com", "pepito1234");
        
        managerRepo.VerifyAll();
        adminRepo.VerifyAll();
        staffRepo.VerifyAll();
    }

    [TestMethod]
    public void CreateSessionOk()
    {
        Guid sessionToken = Guid.NewGuid();
        UsersLogic userService = new UsersLogic(staffRepo.Object, adminRepo.Object, managerRepo.Object, sessionRepo.Object);
        sessionRepo.Setup(sessionRepository => sessionRepository.Create(It.IsAny<Session>()));
        Session session = new Session() { Id = 1, Token = sessionToken, UserEmail = "Pepe@gmail.com" };
        
        Guid? token = userService.CreateSession(session);
        
        sessionRepo.VerifyAll();
        Assert.AreEqual(sessionToken, token);
    }
    
    [TestMethod]
    public void GetCurrentUserWithToken()
    {
        Guid sessionToken = Guid.NewGuid();
        UsersLogic userService = new UsersLogic(staffRepo.Object, adminRepo.Object, managerRepo.Object, sessionRepo.Object);
        sessionRepo.Setup(sessionRepository => sessionRepository.FindByToken(It.IsAny<Guid>())).Returns("pepito@gmail.com");
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        MaintenanceStaff expectedMaintenanceStaff = new MaintenanceStaff()
            { Email = "pepito@gmail.com",Name = "pepito", LastName = "Perez",Id =1, Password = "pepito123" };

        
        MaintenanceStaff returnedUser = (MaintenanceStaff)userService.GetCurrentUser(sessionToken);
        
        sessionRepo.VerifyAll();
        Assert.AreEqual(expectedMaintenanceStaff, returnedUser);
    }
    
    [TestMethod]
    public void GetCurrentUserWithNullToken()
    {
        Guid sessionToken = Guid.NewGuid();
        UsersLogic userService = new UsersLogic(staffRepo.Object, adminRepo.Object, managerRepo.Object, sessionRepo.Object);
        sessionRepo.Setup(sessionRepository => sessionRepository.FindByToken(It.IsAny<Guid>())).Returns("pepito@gmail.com");
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        MaintenanceStaff expectedMaintenanceStaff = new MaintenanceStaff()
            { Email = "pepito@gmail.com",Name = "pepito", LastName = "Perez",Id =1, Password = "pepito123" };

        
        User returnedUser = userService.GetCurrentUser(sessionToken);
        MaintenanceStaff returnedCurrentUser = (MaintenanceStaff)userService.GetCurrentUser(); 
        
        sessionRepo.VerifyAll();
        Assert.AreEqual(expectedMaintenanceStaff, returnedCurrentUser);
    }
}
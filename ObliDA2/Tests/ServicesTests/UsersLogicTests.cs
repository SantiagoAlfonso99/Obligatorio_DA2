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
    private Mock<ICompanyAdminRepository> companyAdminRepo;
    private Mock<IAdminRepository> adminRepo;
    private Mock<ISessionRepository> sessionRepo;
    private List<Manager> managers;
    private List<CompanyAdmin> companyAdmins;
    private List<Admin> admins;
    private List<MaintenanceStaff> personnel;
    private UsersLogic userService;
    
    [TestInitialize]
    public void Initialize()
    {
        sessionRepo = new Mock<ISessionRepository>();
        companyAdminRepo = new Mock<ICompanyAdminRepository>();
        managerRepo = new Mock<IManagerRepository>();
        staffRepo = new Mock<IMaintenanceStaffRepository>();
        adminRepo = new Mock<IAdminRepository>();
        managers = new List<Manager>() { new Manager { Email = "pepe@gmail.com",Name = "pepe",Id =1, Password = "pepe123" } };
        companyAdmins = new List<CompanyAdmin>() { new CompanyAdmin { Email = "pancho@gmail.com",Name = "ignacio",Id =1, Password = "ignacio123" } };
        admins = new List<Admin>() { new Admin { Email = "raul@gmail.com",Name = "raul", LastName = "Rodriguez",Id =1, Password = "raul123" } };
        personnel = new List<MaintenanceStaff>() { new MaintenanceStaff { Email = "pepito@gmail.com",Name = "pepito", LastName = "Perez",Id =1, Password = "pepito123" } };
        userService = new UsersLogic(staffRepo.Object, adminRepo.Object, managerRepo.Object, sessionRepo.Object, companyAdminRepo.Object);
    }
    
    [TestMethod]
    [ExpectedException(typeof(DuplicateEntryException))]
    public void DuplicatedEmailThrowsException()
    {
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        companyAdminRepo.Setup(repository => repository.GetAll()).Returns(companyAdmins);
        
        userService.ValidateEmail("pepe@gmail.com");
        
        managerRepo.VerifyAll();
        adminRepo.VerifyAll();
        staffRepo.VerifyAll();
    }
    
    [TestMethod]
    public void ValidEmailShouldNotThrowAnException()
    {
        bool success = true;
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        companyAdminRepo.Setup(repository => repository.GetAll()).Returns(companyAdmins);
        
        userService.ValidateEmail("pepe3@gmail.com");
        
        managerRepo.VerifyAll();
        adminRepo.VerifyAll();
        staffRepo.VerifyAll();
        Assert.AreEqual(success, true);
    }

    [TestMethod]
    public void FindAdminEmail()
    {
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        companyAdminRepo.Setup(repository => repository.GetAll()).Returns(companyAdmins);
        
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
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        companyAdminRepo.Setup(repository => repository.GetAll()).Returns(companyAdmins);
        
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
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        companyAdminRepo.Setup(repository => repository.GetAll()).Returns(companyAdmins);
        
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
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        companyAdminRepo.Setup(repository => repository.GetAll()).Returns(companyAdmins);
        
        MaintenanceStaff returnedMaintenanceStaff = (MaintenanceStaff)userService.FindUserByEmail("noexiste@gmail.com");
        
        managerRepo.VerifyAll();
        adminRepo.VerifyAll();
        staffRepo.VerifyAll();
    }
    
    [TestMethod]
    public void LogInOk()
    {
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        companyAdminRepo.Setup(repository => repository.GetAll()).Returns(companyAdmins);
        
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
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        companyAdminRepo.Setup(repository => repository.GetAll()).Returns(companyAdmins);
        
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
        sessionRepo.Setup(sessionRepository => sessionRepository.FindByToken(It.IsAny<Guid>())).Returns("pepito@gmail.com");
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        companyAdminRepo.Setup(repository => repository.GetAll()).Returns(companyAdmins);
        
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
        sessionRepo.Setup(sessionRepository => sessionRepository.FindByToken(It.IsAny<Guid>())).Returns("pepito@gmail.com");
        managerRepo.Setup(repository => repository.GetAll()).Returns(managers);
        adminRepo.Setup(repository => repository.GetAll()).Returns(admins);
        staffRepo.Setup(repository => repository.GetAll()).Returns(personnel);
        companyAdminRepo.Setup(repository => repository.GetAll()).Returns(companyAdmins);
        
        MaintenanceStaff expectedMaintenanceStaff = new MaintenanceStaff()
            { Email = "pepito@gmail.com",Name = "pepito", LastName = "Perez",Id =1, Password = "pepito123" };

        
        User returnedUser = userService.GetCurrentUser(sessionToken);
        MaintenanceStaff returnedCurrentUser = (MaintenanceStaff)userService.GetCurrentUser(); 
        
        sessionRepo.VerifyAll();
        Assert.AreEqual(expectedMaintenanceStaff, returnedCurrentUser);
    }
}
using System.Data.Common;
using Domain.Exceptions;
using Domain.Models;
using BusinessLogic.Services;
using BusinessLogic.IRepository;
using IBusinessLogic;
using Moq;

namespace Tests.ServicesTests;

[TestClass]
public class MaintenanceLogicTests
{
    private Mock<IMaintenanceStaffRepository> repo;
    private const int UserId = 1;
    private MaintenanceStaff newStaff = new MaintenanceStaff() { Id = UserId, Email = "pepe@gmail.com"};
    private const bool TrueSuccess = true;
    private const bool FalseSuccess = false;
    
    [TestInitialize]
    public void Initialize()
    {
        repo = new Mock<IMaintenanceStaffRepository>();
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        List<MaintenanceStaff> consultedList = new List<MaintenanceStaff>(){newStaff};
        repo.Setup(repository => repository.GetAll()).Returns(consultedList);
        MaintenanceStaffLogic service = new MaintenanceStaffLogic(repo.Object);

        List<MaintenanceStaff> returnedList = service.GetAll();
        List<MaintenanceStaff> expectedList = new List<MaintenanceStaff>() {newStaff };
        
        repo.VerifyAll();
        CollectionAssert.AreEqual(expectedList, returnedList);
    }    
    
    [TestMethod]
    public void GetByIdOk()
    {
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newStaff);
        MaintenanceStaffLogic service = new MaintenanceStaffLogic(repo.Object);

        MaintenanceStaff returnedStaff = service.GetById(1);
        MaintenanceStaff expectedStaff = new MaintenanceStaff() { Id = UserId, Email = "pepe@gmail.com"};
        
        repo.VerifyAll();
        Assert.AreEqual(expectedStaff, returnedStaff);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidStaffLogicException))]
    public void GetByIdThrowsStaffLogicException()
    {
        newStaff = null;
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newStaff);
        MaintenanceStaffLogic service = new MaintenanceStaffLogic(repo.Object);

        MaintenanceStaff returnedStaff = service.GetById(1);
        MaintenanceStaff expectedStaff = new MaintenanceStaff() { Id = UserId, Email = "pepe@gmail.com"};
        
        repo.VerifyAll();
        Assert.AreEqual(expectedStaff, returnedStaff);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        List<MaintenanceStaff> allStaff = new List<MaintenanceStaff>() { new MaintenanceStaff(){Email = "pepe2@gmail.com"}};
        repo.Setup(repository => repository.GetAll()).Returns(allStaff);
        repo.Setup(repository => repository.Create(It.IsAny<MaintenanceStaff>()));
        MaintenanceStaffLogic service = new MaintenanceStaffLogic(repo.Object);

        MaintenanceStaff returnedStaff = service.Create(newStaff);
        MaintenanceStaff expectedStaff = new MaintenanceStaff() { Id = UserId, Email = "pepe@gmail.com"};
        
        repo.VerifyAll();
        Assert.AreEqual(expectedStaff, returnedStaff);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidStaffLogicException))]
    public void CreateThrowsStaffLogicExceptionOk()
    {
        List<MaintenanceStaff> allStaff = new List<MaintenanceStaff>() { new MaintenanceStaff(){Email = "pepe@gmail.com"}};
        repo.Setup(repository => repository.GetAll()).Returns(allStaff);
        repo.Setup(repository => repository.Create(It.IsAny<MaintenanceStaff>()));
        MaintenanceStaffLogic service = new MaintenanceStaffLogic(repo.Object);

        MaintenanceStaff returnedStaff = service.Create(newStaff);
        MaintenanceStaff expectedStaff = new MaintenanceStaff() { Id = UserId };
        
        repo.VerifyAll();
        Assert.AreEqual(expectedStaff, returnedStaff);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(newStaff);
        repo.Setup(repository => repository.Delete(It.IsAny<MaintenanceStaff>()));
        MaintenanceStaffLogic service = new MaintenanceStaffLogic(repo.Object);

        bool success = service.Delete(UserId);
        
        repo.VerifyAll();
        Assert.AreEqual(TrueSuccess, success);
    }
    
    [TestMethod]
    public void DeleteReturnsFalseForNonExistentId()
    {
        MaintenanceStaff staffToRemove = null;
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(staffToRemove);
        MaintenanceStaffLogic service = new MaintenanceStaffLogic(repo.Object);

        bool success = service.Delete(UserId);
        
        repo.VerifyAll();
        Assert.AreEqual(FalseSuccess, success);
    }
}
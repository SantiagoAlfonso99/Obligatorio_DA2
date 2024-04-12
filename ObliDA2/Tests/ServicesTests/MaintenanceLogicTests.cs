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
    [TestMethod]
    public void GetAllOk()
    {
        Mock<IMaintenanceStaffRepository> repo = new Mock<IMaintenanceStaffRepository>();
        List<MaintenanceStaff> consultedList = new List<MaintenanceStaff>(){new MaintenanceStaff(){Id = 1}};
        repo.Setup(repository => repository.GetAll()).Returns(consultedList);
        MaintenanceStaffLogic service = new MaintenanceStaffLogic(repo.Object);

        List<MaintenanceStaff> returnedList = service.GetAll();
        List<MaintenanceStaff> expectedList = new List<MaintenanceStaff>() { new MaintenanceStaff() { Id = 1 } };
        
        repo.VerifyAll();
        CollectionAssert.AreEqual(expectedList, returnedList);
    }    
    
    [TestMethod]
    public void GetByIdOk()
    {
        Mock<IMaintenanceStaffRepository> repo = new Mock<IMaintenanceStaffRepository>();
        MaintenanceStaff consultedIndividual = new MaintenanceStaff() { Id = 1 };
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(consultedIndividual);
        MaintenanceStaffLogic service = new MaintenanceStaffLogic(repo.Object);

        MaintenanceStaff returnedStaff = service.GetById(1);
        MaintenanceStaff expectedStaff = new MaintenanceStaff() { Id = 1 };
        
        repo.VerifyAll();
        Assert.AreEqual(expectedStaff, returnedStaff);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        Mock<IMaintenanceStaffRepository> repo = new Mock<IMaintenanceStaffRepository>();
        MaintenanceStaff newStaff = new MaintenanceStaff() { Id = 1 };
        repo.Setup(repository => repository.Create(It.IsAny<MaintenanceStaff>()));
        MaintenanceStaffLogic service = new MaintenanceStaffLogic(repo.Object);

        MaintenanceStaff returnedStaff = service.Create(newStaff);
        MaintenanceStaff expectedStaff = new MaintenanceStaff() { Id = 1 };
        
        repo.VerifyAll();
        Assert.AreEqual(expectedStaff, returnedStaff);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Mock<IMaintenanceStaffRepository> repo = new Mock<IMaintenanceStaffRepository>();
        MaintenanceStaff staffToRemove = new MaintenanceStaff() { Id = 1 };
       repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(staffToRemove);
        repo.Setup(repository => repository.Delete(It.IsAny<MaintenanceStaff>()));
        MaintenanceStaffLogic service = new MaintenanceStaffLogic(repo.Object);

        bool success = service.Delete(1);
        
        repo.VerifyAll();
        Assert.AreEqual(true, success);
    }
    
    [TestMethod]
    public void DeleteReturnsFalseForNonExistentId()
    {
        Mock<IMaintenanceStaffRepository> repo = new Mock<IMaintenanceStaffRepository>();
        MaintenanceStaff staffToRemove = null;
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(staffToRemove);
        MaintenanceStaffLogic service = new MaintenanceStaffLogic(repo.Object);

        bool success = service.Delete(1);
        
        repo.VerifyAll();
        Assert.AreEqual(false, success);
    }
}
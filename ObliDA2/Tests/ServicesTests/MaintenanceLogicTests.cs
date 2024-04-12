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
    
}
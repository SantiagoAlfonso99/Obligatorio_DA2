using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogic.Services;
using BusinessLogic.IRepository;
using Domain.Models;
using Domain.Exceptions;
using System.Collections.Generic;
using System.Linq;
namespace Tests.ServicesTests;

[TestClass]
public class ManagerLogicTests
{
    private Mock<IManagerRepository> mockManagerRepo;
    private Mock<IRequestRepository> mockRequestRepo;
    private ManagerLogic managerLogic;
    private Apartment newApartment;
    private Category newCategory;
    private DateTime timeNow;
    private Request returnedRequest;
    
    [TestInitialize]
    public void Setup()
    {
        newApartment = new Apartment(){Id = 1, Building = new Building(){Id = 1}};
        newCategory = new Category() { Name = "name" };
        timeNow = DateTime.Now;
        var description = "New issue";
        var department = new Apartment(){Id =1, Building = new Building(){Id =1}};
        var category = new Category(){Name = "Vecino molesto"};
        returnedRequest = new Request() {Category = category, Description = description, Department = department};
        mockManagerRepo = new Mock<IManagerRepository>();
        mockRequestRepo = new Mock<IRequestRepository>();

   
        managerLogic = new ManagerLogic(mockManagerRepo.Object, mockRequestRepo.Object);
    }

    [TestMethod]
    public void ViewRequests_WithNullCategory_ShouldReturnAllRequests()
    {
   
        var requests = new List<Request>
        {
            new Request { Description = "Light fixture issue", Department = new Apartment(){Id =1}, Category = new Category(){Name = "Electricista"} },
            new Request { Description = "Leaky faucet", Department = new Apartment(){Id =1}, Category = new Category(){Name = "Fontanero"} }
        };
        mockRequestRepo.Setup(repo => repo.GetAll()).Returns(requests);

   
        var result = managerLogic.ViewRequests();

        mockRequestRepo.VerifyAll();
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public void ViewRequests_WithCategory_ShouldFilterRequests()
    {
   
        var requests = new List<Request>
        {
            new Request { Description = "Light fixture issue", Department = new Apartment(){Id =1}, Category = new Category(){Name = "Electricista"} },
            new Request { Description = "Leaky faucet", Department = new Apartment(){Id =1}, Category = new Category(){Name = "Fontanero"} }
        };
        mockRequestRepo.Setup(repo => repo.GetAll()).Returns(requests);

   
        var result = managerLogic.ViewRequests("Fontanero");
        var expectedList = new List<Request>() { new Request { Description = "Leaky faucet", Department = new Apartment(){Id =1}, Category = new Category(){Name = "Fontanero"}}};
        
        mockRequestRepo.VerifyAll();
        CollectionAssert.AreEqual(expectedList, result.ToList());
    }

    [TestMethod]
    public void AssignRequestToMaintenance_ValidRequestAndMaintenance_ShouldReturnTrue()
    {
        mockRequestRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(returnedRequest);

        List<Building> associatedBuildings = new List<Building>()
            { new Building() { Id = 1 }, new Building() { Id = 2 }, new Building() { Id = 3 } };
        MaintenanceStaff newWorker = new MaintenanceStaff() { Id = 1, Buildings = associatedBuildings};
        var result = managerLogic.AssignRequestToMaintenance(1, newWorker);

        mockRequestRepo.VerifyAll();
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void AssignRequestToMaintenance_InvalidBuildingAssociated_ShouldReturnFalse()
    {
        this.returnedRequest.Department.Building.Id = 8;
        mockRequestRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(returnedRequest);

        List<Building> associatedBuildings = new List<Building>()
            { new Building() { Id = 1 }, new Building() { Id = 2 }, new Building() { Id = 3 } };
        MaintenanceStaff newWorker = new MaintenanceStaff() { Id = 1, Buildings = associatedBuildings};
        var result = managerLogic.AssignRequestToMaintenance(1, newWorker);

        mockRequestRepo.VerifyAll();
        mockManagerRepo.VerifyAll();
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    public void AssignRequestToMaintenance_PendingStatusRequest_ShouldReturnFalse()
    {
        returnedRequest.Status = RequestStatus.Attending;
        mockRequestRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(returnedRequest);

        List<Building> associatedBuildings = new List<Building>()
            { new Building() { Id = 1 }, new Building() { Id = 2 }, new Building() { Id = 3 } };
        MaintenanceStaff newWorker = new MaintenanceStaff() { Id = 1, Buildings = associatedBuildings};
        var result = managerLogic.AssignRequestToMaintenance(1, newWorker);

        mockRequestRepo.VerifyAll();
        mockManagerRepo.VerifyAll();
        Assert.IsFalse(result);
    }
    
    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void AssignRequestToMaintenance_InvalidRequestOrMaintenance_ShouldThrowException()
    {
        Request returnedRequest = null;
        mockRequestRepo.Setup(x => x.Get(It.IsAny<int>())).Returns(returnedRequest);
        
        MaintenanceStaff newWorker = new MaintenanceStaff() { Id = 1 };
        managerLogic.AssignRequestToMaintenance(1, newWorker);
        
        mockRequestRepo.VerifyAll();
    }

    [TestMethod]
    public void CreateRequest_ShouldCreateAndReturnRequest()
    {
   
        var description = "New issue";
        var department = new Apartment(){Id =1, Building = new Building(){Id =1}};
        var category = "Vecino molesto";
        var building = new Building() { Id = 1 };
   
        var result = managerLogic.CreateRequest(description, department, new Category(){Name = category});

   
        Assert.IsNotNull(result);
        Assert.AreEqual("Open", result.Status.ToString());
        Assert.AreEqual(description, result.Description);
        Assert.AreEqual(category, result.Category.Name);
    }

    [TestMethod]
    public void CreateManagerOk()
    {
        mockManagerRepo.Setup(repository => repository.Add(It.IsAny<Manager>()));

        Manager newManager = managerLogic.Create(new Manager(){Name = "pepe", Email = "pepe@gmail.com", Password = "Password"});
        Manager expectedManager = new Manager() { Name = "pepe", Email = "pepe@gmail.com", Password = "Password" };
        
        mockManagerRepo.VerifyAll();
        Assert.AreEqual(expectedManager, newManager);
    }
    
    [TestMethod]
    public void GetManagerOk()
    {
        Manager newManager = new Manager(){Name = "pepe", Email = "pepe@gmail.com", Password = "Password"};
        mockManagerRepo.Setup(repository => repository.Get(It.IsAny<int>())).Returns(newManager);

        Manager returnedManager = managerLogic.GetById(1);
        Manager expectedManager = new Manager() { Name = "pepe", Email = "pepe@gmail.com", Password = "Password" };
        
        mockManagerRepo.VerifyAll();
        Assert.AreEqual(expectedManager, newManager);
    }
    
    [TestMethod]
    [ExpectedException(typeof(NotFoundException))]
    public void GetManagerThrowsException()
    {
        Manager newManager = null;
        mockManagerRepo.Setup(repository => repository.Get(It.IsAny<int>())).Returns(newManager);

        Manager returnedManager = managerLogic.GetById(1);
        Manager expectedManager = new Manager() { Name = "pepe", Email = "pepe@gmail.com", Password = "Password" };
        
        mockManagerRepo.VerifyAll();
        Assert.AreEqual(expectedManager, newManager);
    }

    [TestMethod]
    public void MaintenanceStaffAcceptInvitationOk()
    { 
        Request createdRequest = new Request()
        {
            Id = 3, Department = newApartment, Status = RequestStatus.Open, Category = newCategory,
            Description = "El vecino no para de gritar", AssignedToMaintenanceId = 1
        };
        Request expectedRequest = new Request()
        {
            Id = 3, Department = newApartment, Status = RequestStatus.Attending, Category = newCategory,
            Description = "El vecino no para de gritar", AssignedToMaintenanceId = 1, Service_start = timeNow
        };
        mockRequestRepo.Setup(repository => repository.Get(It.IsAny<int>())).Returns(createdRequest);
        mockRequestRepo.Setup(repository => repository.Update(It.IsAny<Request>()));
        
        Request returnedRequest = managerLogic.MaintenanceStaffAcceptRequest(createdRequest, timeNow);
        
        mockRequestRepo.VerifyAll();
        Assert.AreEqual(expectedRequest, returnedRequest);
    }
    
    [TestMethod]
    public void MaintenanceStaffCompleteInvitationOk()
    { 
        Request createdRequest = new Request()
        {
            Id = 3, Department = newApartment, Status = RequestStatus.Attending, Category = newCategory,
            Description = "El vecino no para de gritar", AssignedToMaintenanceId = 1, Service_start = timeNow.AddDays(-1)
        };
        Request expectedRequest = new Request()
        {
            Id = 3, Department = newApartment, Status = RequestStatus.Closed, Category = newCategory,
            Description = "El vecino no para de gritar", AssignedToMaintenanceId = 1, Service_start = timeNow.AddDays(-1),
            FinalCost = 500, Service_end = timeNow
        };
        mockRequestRepo.Setup(repository => repository.Update(It.IsAny<Request>()));
        
        Request returnedRequest = managerLogic.MaintenanceStaffCompleteRequest(createdRequest, 500, timeNow);
        
        mockRequestRepo.VerifyAll();
        Assert.AreEqual(expectedRequest, returnedRequest);
    }
}

using BusinessLogic.Services;
using BusinessLogic.IRepository;
using Domain.Models;
using IBusinessLogic;
using Moq;

namespace Tests.ServicesTests;

[TestClass]
public class ReportLogicTests
{
    private Mock<IBuildingRepository> buildingRepo;
    private Mock<IRequestRepository> requestRepo;
    private Mock<IManagerRepository> managerRepo;
    private Mock<IMaintenanceStaffRepository> staffRepo;
    
    [TestInitialize]
    public void Initialize()
    {
        buildingRepo = new Mock<IBuildingRepository>();
        requestRepo = new Mock<IRequestRepository>();
        managerRepo = new Mock<IManagerRepository>();
        staffRepo = new Mock<IMaintenanceStaffRepository>();
    }
    
    [TestMethod]
    public void CreateRequestsPerBuildingReports_ReturnsCorrectReports()
    {
        BuildingLogic buildingService = new BuildingLogic(buildingRepo.Object);
        ManagerLogic managerService = new ManagerLogic(managerRepo.Object, requestRepo.Object);
        MaintenanceStaffLogic staffService = new MaintenanceStaffLogic(staffRepo.Object);
        
        var building1 = new Building { Id = 1, Name = "Building 1" };
        var building2 = new Building { Id = 2, Name = "Building 2" };

        var requests = new List<Request>
        {
            new Request { Id = 1, Department = new Apartment(){Id =1, BuildingId = 1}, Status = RequestStatus.Open },
            new Request { Id = 2, Department = new Apartment(){Id =1, BuildingId = 1}, Status = RequestStatus.Closed },
            new Request { Id = 3, Department = new Apartment(){Id =2, BuildingId = 2}, Status = RequestStatus.Attending },
            new Request { Id = 4, Department = new Apartment(){Id =2, BuildingId = 2}, Status = RequestStatus.Closed }
        };

        buildingRepo.Setup(b => b.GetAll()).Returns(new List<Building> { building1, building2 });
        requestRepo.Setup(m => m.GetAll()).Returns(requests);

        var reportLogic = new ReportLogic(managerService, buildingService, staffService);
        
        var reports = reportLogic.CreateRequestsPerBuildingReports("");
        
        var expectedReports = new List<RequestsPerBuildingReport>
        {
            new RequestsPerBuildingReport { BuildingName = "Building 1", OpenRequests = 1, AttendingRequests = 0, ClosedRequests = 1 },
            new RequestsPerBuildingReport { BuildingName = "Building 2", OpenRequests = 0, AttendingRequests = 1, ClosedRequests = 1 }
        };
        
        buildingRepo.VerifyAll();
        requestRepo.VerifyAll();
        CollectionAssert.AreEqual(expectedReports, reports);
    }
    
    [TestMethod]
    public void CreateRequestsPerBuildingReportsWithOthersInputs_ReturnsCorrectReports()
    {
        BuildingLogic buildingService = new BuildingLogic(buildingRepo.Object);
        ManagerLogic managerService = new ManagerLogic(managerRepo.Object, requestRepo.Object);
        MaintenanceStaffLogic staffService = new MaintenanceStaffLogic(staffRepo.Object);
        
        var building3 = new Building { Id = 3, Name = "Building 3" };
        var building4 = new Building { Id = 4, Name = "Building 4" };
        var building5 = new Building { Id = 5, Name = "Building 5" };

        var requests = new List<Request>
        {
            new Request { Id = 5, Department = new Apartment(){Id = 1, BuildingId = 3}, Status = RequestStatus.Open },
            new Request { Id = 6, Department = new Apartment(){Id = 1, BuildingId = 3}, Status = RequestStatus.Attending },
            new Request { Id = 7, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Open },
            new Request { Id = 8, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Closed },
            new Request { Id = 9, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Closed },
            new Request { Id = 10, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Closed }
        };

        buildingRepo.Setup(b => b.GetAll()).Returns(new List<Building> { building3, building4, building5 });
        requestRepo.Setup(m => m.GetAll()).Returns(requests);

        var reportLogic = new ReportLogic(managerService, buildingService, staffService);
        
        var reports = reportLogic.CreateRequestsPerBuildingReports("");
        
        var expectedReports = new List<RequestsPerBuildingReport>
        {
            new RequestsPerBuildingReport { BuildingName = "Building 3", OpenRequests = 1, AttendingRequests = 1, ClosedRequests = 0 },
            new RequestsPerBuildingReport { BuildingName = "Building 4", OpenRequests = 1, AttendingRequests = 0, ClosedRequests = 3 },
            new RequestsPerBuildingReport { BuildingName = "Building 5", OpenRequests = 0, AttendingRequests = 0, ClosedRequests = 0 }
        };
        
        buildingRepo.VerifyAll();
        requestRepo.VerifyAll();
        CollectionAssert.AreEqual(expectedReports, reports);
    }
    
    [TestMethod]
    public void CreateRequestsPerBuildingReports_ReturnsCorrectFilteredReport()
    {
        BuildingLogic buildingService = new BuildingLogic(buildingRepo.Object);
        ManagerLogic managerService = new ManagerLogic(managerRepo.Object, requestRepo.Object);
        MaintenanceStaffLogic staffService = new MaintenanceStaffLogic(staffRepo.Object);

        var building3 = new Building { Id = 3, Name = "Building 3" };
        var building4 = new Building { Id = 4, Name = "Building 4" };
        var building5 = new Building { Id = 5, Name = "Building 5" };

        var requests = new List<Request>
        {
            new Request { Id = 5, Department = new Apartment(){Id = 1, BuildingId = 3}, Status = RequestStatus.Open },
            new Request { Id = 6, Department = new Apartment(){Id = 1, BuildingId = 3}, Status = RequestStatus.Attending },
            new Request { Id = 7, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Open },
            new Request { Id = 8, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Closed },
            new Request { Id = 9, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Closed },
            new Request { Id = 10, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Closed }
        };

        buildingRepo.Setup(b => b.GetAll()).Returns(new List<Building> { building3, building4, building5 });
        requestRepo.Setup(m => m.GetAll()).Returns(requests);

        var reportLogic = new ReportLogic(managerService, buildingService, staffService);
        
        var reports = reportLogic.CreateRequestsPerBuildingReports("Building 4");
        
        var expectedReports = new List<RequestsPerBuildingReport>
        {
            new RequestsPerBuildingReport { BuildingName = "Building 4", OpenRequests = 1, AttendingRequests = 0, ClosedRequests = 3 },
        };
        
        buildingRepo.VerifyAll();
        requestRepo.VerifyAll();
        CollectionAssert.AreEqual(expectedReports, reports);
    }
    
    [TestMethod]
    public void CreateRequestsPerMaintenanceStaffReport_ReturnsCorrectValues()
    {
        BuildingLogic buildingService = new BuildingLogic(buildingRepo.Object);
        ManagerLogic managerService = new ManagerLogic(managerRepo.Object, requestRepo.Object);
        MaintenanceStaffLogic staffService = new MaintenanceStaffLogic(staffRepo.Object);
        DateTime now = DateTime.Now;
        
        
        var building3 = new Building { Id = 3, Name = "Building 3" };
        var building4 = new Building { Id = 4, Name = "Building 4" };
        var building5 = new Building { Id = 5, Name = "Building 5" };

        List<MaintenanceStaff> mockWorkers = new List<MaintenanceStaff>
        {
            new MaintenanceStaff { Id = 1, Name = "Worker 1", AssociatedBuilding = new Building { Id = 4 } },
            new MaintenanceStaff { Id = 2, Name = "Worker 2", AssociatedBuilding = new Building { Id = 4 } }
        };
        
        var requests = new List<Request>
        {
            new Request { Id = 5, Department = new Apartment(){Id = 1, BuildingId = 3}, Status = RequestStatus.Open },
            new Request { Id = 6, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Attending, Service_start = DateTime.Now.AddHours(-2), AssignedToMaintenanceId = 1},
            new Request { Id = 7, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Open, AssignedToMaintenanceId = 1 },
            new Request { Id = 8, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Closed, Service_start = now.AddHours(-2), Service_end = now, AssignedToMaintenanceId = 1},
            new Request { Id = 9, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Closed, Service_start = now.AddHours(-6), Service_end = now, AssignedToMaintenanceId = 2 },
            new Request { Id = 10, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Closed, Service_start = now.AddHours(-9), Service_end = now, AssignedToMaintenanceId = 2 }
        };
        staffRepo.Setup(repository => repository.GetAll()).Returns(mockWorkers);
        buildingRepo.Setup(b => b.GetAll()).Returns(new List<Building> { building3, building4, building5 });
        requestRepo.Setup(m => m.GetAll()).Returns(requests);

        var reportLogic = new ReportLogic(managerService, buildingService, staffService);
        
        var reports = reportLogic.CreateRequestsPerMaintenanceStaffReports("",4);
        
        var expectedReports = new List<RequestsPerMaintenanceStaffReport>
        {
            new RequestsPerMaintenanceStaffReport
            {
                MaintenanceWorker = "Worker 1",
                OpenRequests = 1,
                AttendingRequests = 1,
                ClosedRequests = 1,
                AverageClosingTime = 2/1
            },
            new RequestsPerMaintenanceStaffReport
            {
                MaintenanceWorker = "Worker 2",
                OpenRequests = 0,
                AttendingRequests = 0,
                ClosedRequests = 2,
                AverageClosingTime = (6 + 9) / 2 
            } 
        };
        staffRepo.VerifyAll();
        requestRepo.VerifyAll();
        CollectionAssert.AreEqual(expectedReports, reports);
    }
    
    [TestMethod]
    public void CreateRequestsPerMaintenanceStaffReport_ReturnsCorrectReport()
    {
        BuildingLogic buildingService = new BuildingLogic(buildingRepo.Object);
        ManagerLogic managerService = new ManagerLogic(managerRepo.Object, requestRepo.Object);
        MaintenanceStaffLogic staffService = new MaintenanceStaffLogic(staffRepo.Object);
        DateTime now = DateTime.Now;
        
        
        var building3 = new Building { Id = 3, Name = "Building 3" };
        var building4 = new Building { Id = 4, Name = "Building 4" };
        var building5 = new Building { Id = 5, Name = "Building 5" };

        List<MaintenanceStaff> mockWorkers = new List<MaintenanceStaff>
        {
            new MaintenanceStaff { Id = 1, Name = "Worker 1", AssociatedBuilding = new Building { Id = 4 } },
            new MaintenanceStaff { Id = 2, Name = "Worker 2", AssociatedBuilding = new Building { Id = 4 } }
        };
        
        var requests = new List<Request>
        {
            new Request { Id = 5, Department = new Apartment(){Id = 1, BuildingId = 3}, Status = RequestStatus.Open },
            new Request { Id = 6, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Attending, Service_start = DateTime.Now.AddHours(-2), AssignedToMaintenanceId = 1},
            new Request { Id = 7, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Open, AssignedToMaintenanceId = 1 },
            new Request { Id = 8, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Closed, Service_start = now.AddHours(-2), Service_end = now, AssignedToMaintenanceId = 1},
            new Request { Id = 9, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Closed, Service_start = now.AddHours(-6), Service_end = now, AssignedToMaintenanceId = 2 },
            new Request { Id = 10, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Closed, Service_start = now.AddHours(-9), Service_end = now, AssignedToMaintenanceId = 2 },
            new Request { Id = 11, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Open, AssignedToMaintenanceId = 2 },
            new Request { Id = 12, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Closed, Service_start = now.AddHours(-4), Service_end = now, AssignedToMaintenanceId = 2 },
            new Request { Id = 13, Department = new Apartment(){Id = 1, BuildingId = 4}, Status = RequestStatus.Closed, Service_start = now.AddHours(-8), Service_end = now, AssignedToMaintenanceId = 2 }
        };
        staffRepo.Setup(repository => repository.GetAll()).Returns(mockWorkers);
        buildingRepo.Setup(b => b.GetAll()).Returns(new List<Building> { building3, building4, building5 });
        requestRepo.Setup(m => m.GetAll()).Returns(requests);

        var reportLogic = new ReportLogic(managerService, buildingService, staffService);
        
        var reports = reportLogic.CreateRequestsPerMaintenanceStaffReports("Worker 2",4);
        
        var expectedReports = new List<RequestsPerMaintenanceStaffReport>
        {
            new RequestsPerMaintenanceStaffReport
            {
                MaintenanceWorker = "Worker 2",
                OpenRequests = 1,
                AttendingRequests = 0,
                ClosedRequests = 4,
                AverageClosingTime = (6 + 9 + 4 + 8) / 4 
            } 
        };
        staffRepo.VerifyAll();
        requestRepo.VerifyAll();
        CollectionAssert.AreEqual(expectedReports, reports);
    }
}
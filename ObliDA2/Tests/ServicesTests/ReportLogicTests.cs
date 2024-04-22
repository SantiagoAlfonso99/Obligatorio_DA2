using BusinessLogic.Services;
using BusinessLogic.IRepository;
using Domain.Models;
using Moq;

namespace Tests.ServicesTests;

[TestClass]
public class ReportLogicTests
{
    [TestMethod]
    public void CreateRequestsPerBuildingReports_ReturnsCorrectReports()
    {
        Mock<IBuildingRepository> buildingRepo = new Mock<IBuildingRepository>();
        Mock<IRequestRepository> requestRepo = new Mock<IRequestRepository>();
        Mock<IManagerRepository> managerRepo = new Mock<IManagerRepository>();
        
        BuildingLogic buildingService = new BuildingLogic(buildingRepo.Object);
        ManagerLogic managerService = new ManagerLogic(managerRepo.Object, requestRepo.Object);

        var building1 = new Building { Id = 1, Name = "Building 1" };
        var building2 = new Building { Id = 2, Name = "Building 2" };

        var requests = new List<Request>
        {
            new Request { Id = 1, BuildingAssociatedId = 1, Status = RequestStatus.Open },
            new Request { Id = 2, BuildingAssociatedId = 1, Status = RequestStatus.Closed },
            new Request { Id = 3, BuildingAssociatedId = 2, Status = RequestStatus.Attending },
            new Request { Id = 4, BuildingAssociatedId = 2, Status = RequestStatus.Closed }
        };

        buildingRepo.Setup(b => b.GetAll()).Returns(new List<Building> { building1, building2 });
        requestRepo.Setup(m => m.GetAll()).Returns(requests);

        var reportLogic = new ReportLogic(managerService, buildingService);
        
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
        Mock<IBuildingRepository> buildingRepo = new Mock<IBuildingRepository>();
        Mock<IRequestRepository> requestRepo = new Mock<IRequestRepository>();
        Mock<IManagerRepository> managerRepo = new Mock<IManagerRepository>();
        
        BuildingLogic buildingService = new BuildingLogic(buildingRepo.Object);
        ManagerLogic managerService = new ManagerLogic(managerRepo.Object, requestRepo.Object);

        var building3 = new Building { Id = 3, Name = "Building 3" };
        var building4 = new Building { Id = 4, Name = "Building 4" };
        var building5 = new Building { Id = 5, Name = "Building 5" };

        var requests = new List<Request>
        {
            new Request { Id = 5, BuildingAssociatedId = 3, Status = RequestStatus.Open },
            new Request { Id = 6, BuildingAssociatedId = 3, Status = RequestStatus.Attending },
            new Request { Id = 7, BuildingAssociatedId = 4, Status = RequestStatus.Open },
            new Request { Id = 8, BuildingAssociatedId = 4, Status = RequestStatus.Closed },
            new Request { Id = 9, BuildingAssociatedId = 4, Status = RequestStatus.Closed },
            new Request { Id = 10, BuildingAssociatedId = 4, Status = RequestStatus.Closed }
        };

        buildingRepo.Setup(b => b.GetAll()).Returns(new List<Building> { building3, building4, building5 });
        requestRepo.Setup(m => m.GetAll()).Returns(requests);

        var reportLogic = new ReportLogic(managerService, buildingService);
        
        var reports = reportLogic.CreateRequestsPerBuildingReports("");
        
        var expectedReports = new List<RequestsPerBuildingReport>
        {
            new RequestsPerBuildingReport { BuildingName = "Building 3", OpenRequests = 1, AttendingRequests = 1, ClosedRequests = 0 },
            new RequestsPerBuildingReport { BuildingName = "Building 4", OpenRequests = 1, AttendingRequests = 0, ClosedRequests = 3 },
            new RequestsPerBuildingReport { BuildingName = "Building 5", OpenRequests = 0, AttendingRequests = 0, ClosedRequests = 0 }
        };
        
        CollectionAssert.AreEqual(expectedReports, reports);
    }
    
    [TestMethod]
    public void CreateRequestsPerBuildingReports_ReturnsCorrectFilteredReport()
    {
        Mock<IBuildingRepository> buildingRepo = new Mock<IBuildingRepository>();
        Mock<IRequestRepository> requestRepo = new Mock<IRequestRepository>();
        Mock<IManagerRepository> managerRepo = new Mock<IManagerRepository>();
        
        BuildingLogic buildingService = new BuildingLogic(buildingRepo.Object);
        ManagerLogic managerService = new ManagerLogic(managerRepo.Object, requestRepo.Object);

        var building3 = new Building { Id = 3, Name = "Building 3" };
        var building4 = new Building { Id = 4, Name = "Building 4" };
        var building5 = new Building { Id = 5, Name = "Building 5" };

        var requests = new List<Request>
        {
            new Request { Id = 5, BuildingAssociatedId = 3, Status = RequestStatus.Open },
            new Request { Id = 6, BuildingAssociatedId = 3, Status = RequestStatus.Attending },
            new Request { Id = 7, BuildingAssociatedId = 4, Status = RequestStatus.Open },
            new Request { Id = 8, BuildingAssociatedId = 4, Status = RequestStatus.Closed },
            new Request { Id = 9, BuildingAssociatedId = 4, Status = RequestStatus.Closed },
            new Request { Id = 10, BuildingAssociatedId = 4, Status = RequestStatus.Closed }
        };

        buildingRepo.Setup(b => b.GetAll()).Returns(new List<Building> { building3, building4, building5 });
        requestRepo.Setup(m => m.GetAll()).Returns(requests);

        var reportLogic = new ReportLogic(managerService, buildingService);
        
        var reports = reportLogic.CreateRequestsPerBuildingReports("Building 4");
        
        var expectedReports = new List<RequestsPerBuildingReport>
        {
            new RequestsPerBuildingReport { BuildingName = "Building 4", OpenRequests = 1, AttendingRequests = 0, ClosedRequests = 3 },
        };
        CollectionAssert.AreEqual(expectedReports, reports);
    }
}
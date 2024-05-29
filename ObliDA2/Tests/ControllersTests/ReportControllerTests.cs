using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;
using WebApi.DTOs.In;

namespace Tests.ControllersTests;

[TestClass]
public class ReportControllerTests
{
    [TestMethod]
    public void GetRequestPerBuildingOk()
    {
        Mock<IReportLogic> reportService = new Mock<IReportLogic>();
        ReportController controller = new ReportController(reportService.Object);

        var returnedReport = new List<RequestsPerBuildingReport>
        {
            new RequestsPerBuildingReport { BuildingName = "Building 1", OpenRequests = 1, AttendingRequests = 0, ClosedRequests = 1 },
            new RequestsPerBuildingReport { BuildingName = "Building 2", OpenRequests = 0, AttendingRequests = 1, ClosedRequests = 1 }
        };
        
        var expectedReport = new List<RequestsPerBuildingReport>
        {
            new RequestsPerBuildingReport { BuildingName = "Building 1", OpenRequests = 1, AttendingRequests = 0, ClosedRequests = 1 },
            new RequestsPerBuildingReport { BuildingName = "Building 2", OpenRequests = 0, AttendingRequests = 1, ClosedRequests = 1 }
        };

        BuildingReportDTO buildingReportDto = new BuildingReportDTO() { BuildingName = "name" };
        reportService.Setup(logic => logic.CreateRequestsPerBuildingReports(It.IsAny<string>())).Returns(returnedReport);
        var result = controller.GetRequestPerBuilding(buildingReportDto);
        var okResult = result as OkObjectResult;
        List<RequestsPerBuildingReport> controllerResult = okResult.Value as List<RequestsPerBuildingReport>;
        
        CollectionAssert.AreEqual(expectedReport, controllerResult);
    }
    
    [TestMethod]
    public void GetRequestPerMaintenanceStaffOk()
    {
        Mock<IReportLogic> reportService = new Mock<IReportLogic>();
        ReportController controller = new ReportController(reportService.Object);
        
        
        var returnedReports = new List<RequestsPerMaintenanceStaffReport>
        {
            new RequestsPerMaintenanceStaffReport { MaintenanceWorker = "Worker 1", OpenRequests = 1, AttendingRequests = 1, ClosedRequests = 1, AverageClosingTime = 2/1 },
            new RequestsPerMaintenanceStaffReport { MaintenanceWorker = "Worker 2", OpenRequests = 0, AttendingRequests = 0, ClosedRequests = 2, AverageClosingTime = (6 + 9) / 2 }
        };

        var expectedReports = new List<RequestsPerMaintenanceStaffReport>
        {
            new RequestsPerMaintenanceStaffReport { MaintenanceWorker = "Worker 1", OpenRequests = 1, AttendingRequests = 1, ClosedRequests = 1, AverageClosingTime = 2/1 },
            new RequestsPerMaintenanceStaffReport { MaintenanceWorker = "Worker 2", OpenRequests = 0, AttendingRequests = 0, ClosedRequests = 2, AverageClosingTime = (6 + 9) / 2 }
        };
        
        reportService.Setup(logic => logic.CreateRequestsPerMaintenanceStaffReports(It.IsAny<string>(),It.IsAny<int>())).Returns(returnedReports);
        var requestModel = new MaintenanceRequestModel
        {
            WorkerName = "name",
            BuildingId = 1
        };

        var result = controller.GetRequestsPerMaintenanceStaff(requestModel);
        var okResult = result as OkObjectResult;
        List<RequestsPerMaintenanceStaffReport> controllerResult = okResult.Value as List<RequestsPerMaintenanceStaffReport>;
        
        CollectionAssert.AreEqual(expectedReports, controllerResult);
    }
}
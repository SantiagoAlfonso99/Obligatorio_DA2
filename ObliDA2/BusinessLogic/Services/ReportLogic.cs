using System.Numerics;
using Domain.Models;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class ReportLogic : IReportLogic
{
    private const double DoubleMinValue = 0;
    private const int Incrementer = 1;
    private const string EmptyString = "";
    
    private IManagerLogic managerLogic;
    private IBuildingLogic buildingLogic;
    private IMaintenanceLogic staffLogic;
    
    public ReportLogic(IManagerLogic managerLogicIn, IBuildingLogic buildingLogicIn, IMaintenanceLogic staffLogicIn)
    {
        managerLogic = managerLogicIn;
        buildingLogic = buildingLogicIn;
        staffLogic = staffLogicIn;
    }
    
    public List<RequestsPerBuildingReport> CreateRequestsPerBuildingReports(string buildingName)
    {
        List<Building> returnedBuildings = buildingLogic.GetAll();
        IEnumerable<Request> returnedRequests = managerLogic.GetAllRequest();
        List<RequestsPerBuildingReport> reports = new List<RequestsPerBuildingReport>();
        foreach (var building in returnedBuildings)
        {
            RequestsPerBuildingReport newReport = new RequestsPerBuildingReport(building.Name);
            foreach (var request in returnedRequests)
            {
                if (request.Department.BuildingId == building.Id)
                {
                    if (request.Status == RequestStatus.Open)
                    {
                        newReport.OpenRequests = newReport.OpenRequests + Incrementer;
                    }
                    else if (request.Status == RequestStatus.Attending)
                    {
                        newReport.AttendingRequests = newReport.AttendingRequests + Incrementer;
                    }
                    else if (request.Status == RequestStatus.Closed)
                    {
                        newReport.ClosedRequests = newReport.ClosedRequests + Incrementer;   
                    }
                }
            }
            reports.Add(newReport);
        }
        if (buildingName == EmptyString)
        {
            return reports;   
        }
        RequestsPerBuildingReport report = reports.FirstOrDefault(buildingReport => buildingReport.BuildingName == buildingName);
        return new List<RequestsPerBuildingReport>() { report};
    }
    
    public List<RequestsPerMaintenanceStaffReport> CreateRequestsPerMaintenanceStaffReports(string workerName, int buildingId)
    {
        List<MaintenanceStaff> workers = staffLogic.GetAll();

        var filteredWorkers = workers
            .Where(worker => worker.Buildings.Any(building => building.Id == buildingId))
            .ToList();
        IEnumerable<Request> returnedRequests = managerLogic.GetAllRequest().Where(request => request.Department.BuildingId == buildingId).ToList();
        List<RequestsPerMaintenanceStaffReport> reports = new List<RequestsPerMaintenanceStaffReport>();
        foreach (var worker in filteredWorkers)
        {
            double closedRequestCounter = DoubleMinValue;
            double totalClosingTime = DoubleMinValue;
            RequestsPerMaintenanceStaffReport newReport = new RequestsPerMaintenanceStaffReport(worker.Name);
            foreach (var request in returnedRequests)
            {
                if (request.AssignedToMaintenanceId == worker.Id)
                {
                    if (request.Status == RequestStatus.Open)
                    {
                        newReport.OpenRequests = newReport.OpenRequests + Incrementer;
                    }
                    else if (request.Status == RequestStatus.Attending)
                    {
                        newReport.AttendingRequests = newReport.AttendingRequests + Incrementer;
                    }
                    else if (request.Status == RequestStatus.Closed)
                    {
                        closedRequestCounter++;
                        TimeSpan serviceDuration = request.Service_end - request.Service_start;
                        totalClosingTime = totalClosingTime + serviceDuration.TotalHours;
                        newReport.ClosedRequests = newReport.ClosedRequests + Incrementer;   
                    }   
                }
            }
            if (closedRequestCounter > DoubleMinValue)
            {
                newReport.AverageClosingTime = totalClosingTime / closedRequestCounter;
            }
            reports.Add(newReport);
        }
        if (workerName == EmptyString)
        {
            return reports;   
        }
        RequestsPerMaintenanceStaffReport report = reports.FirstOrDefault(buildingReport => buildingReport.MaintenanceWorker == workerName );
        return new List<RequestsPerMaintenanceStaffReport>() {report};
    }
}
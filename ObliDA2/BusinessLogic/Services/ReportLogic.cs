using System.Numerics;
using Domain.Models;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class ReportLogic : IReportLogic
{
    private const int MinValue = 0;
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
            RequestsPerBuildingReport newReport = new RequestsPerBuildingReport();
            newReport.BuildingName = building.Name;
            newReport.AttendingRequests = MinValue;
            newReport.ClosedRequests = MinValue;
            newReport.OpenRequests = MinValue;
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
        List<MaintenanceStaff> workers = staffLogic.GetAll().Where(worker => worker.AssociatedBuilding.Id == buildingId).ToList();
        IEnumerable<Request> returnedRequests = managerLogic.GetAllRequest().Where(request => request.Department.BuildingId == buildingId).ToList();
        List<RequestsPerMaintenanceStaffReport> reports = new List<RequestsPerMaintenanceStaffReport>();
        foreach (var worker in workers)
        {
            double closedRequestCounter = MinValue;
            double totalClosingTime = MinValue;
            RequestsPerMaintenanceStaffReport newReport = new RequestsPerMaintenanceStaffReport();
            newReport.MaintenanceWorker = worker.Name;
            newReport.AttendingRequests = MinValue;
            newReport.ClosedRequests = MinValue;
            newReport.OpenRequests = MinValue;
            newReport.AverageClosingTime = MinValue;
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
                        double closingTimeInHours = (request.Service_end - request.Service_start).TotalHours;
                        totalClosingTime = totalClosingTime + closingTimeInHours;
                        newReport.ClosedRequests = newReport.ClosedRequests + Incrementer;   
                    }   
                }
            }
            newReport.AverageClosingTime = totalClosingTime / closedRequestCounter;
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
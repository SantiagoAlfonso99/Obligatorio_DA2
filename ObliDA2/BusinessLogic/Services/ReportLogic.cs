using Domain.Models;

namespace BusinessLogic.Services;

public struct RequestsPerBuildingReport
{
    public string BuildingName { get; set; }
    public int OpenRequests { get; set; }
    public int AttendingRequests { get; set; }
    public int ClosedRequests { get; set; }
    
    public override bool Equals(object obj)
    {
        var other = (RequestsPerBuildingReport)obj;
        return BuildingName == other.BuildingName &&
               OpenRequests == other.OpenRequests &&
               AttendingRequests == other.AttendingRequests &&
               ClosedRequests == other.ClosedRequests;
    }
}

public class ReportLogic
{
    private ManagerLogic managerLogic;
    private BuildingLogic buildingLogic;

    public ReportLogic(ManagerLogic managerLogicIn, BuildingLogic buildingLogicIn)
    {
        managerLogic = managerLogicIn;
        buildingLogic = buildingLogicIn;
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
            newReport.AttendingRequests = 0;
            newReport.ClosedRequests = 0;
            newReport.OpenRequests = 0;
            foreach (var request in returnedRequests)
            {
                if (request.BuildingAssociatedId == building.Id)
                {
                    if (request.Status == RequestStatus.Open)
                    {
                        newReport.OpenRequests = newReport.OpenRequests + 1;
                    }
                    else if (request.Status == RequestStatus.Attending)
                    {
                        newReport.AttendingRequests = newReport.AttendingRequests + 1;
                    }
                    else if (request.Status == RequestStatus.Closed)
                    {
                        newReport.ClosedRequests = newReport.ClosedRequests + 1;   
                    }
                }
            }
            reports.Add(newReport);
        }
        if (buildingName == "")
        {
            return reports;   
        }
        RequestsPerBuildingReport report = reports.FirstOrDefault(buildingReport => buildingReport.BuildingName == buildingName);
        return new List<RequestsPerBuildingReport>() { report};
    }
}
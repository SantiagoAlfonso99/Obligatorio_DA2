namespace Domain.Models;

public class RequestsPerBuildingReport
{
    private const int MinValue = 0;
    
    public RequestsPerBuildingReport(){}
    public RequestsPerBuildingReport(string buildingName)
    {
        this.BuildingName = buildingName;
        this.AttendingRequests = MinValue;
        this.ClosedRequests = MinValue;
        this.OpenRequests = MinValue;
    }
    
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
namespace Domain.Models;

public class RequestsPerBuildingReport
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
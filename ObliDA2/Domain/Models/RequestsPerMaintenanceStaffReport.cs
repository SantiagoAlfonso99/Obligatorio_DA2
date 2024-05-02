namespace Domain.Models;

public class RequestsPerMaintenanceStaffReport
{
    public string MaintenanceWorker { get; set; }
    public int OpenRequests { get; set; }
    public int AttendingRequests { get; set; }
    public int ClosedRequests { get; set; }
    public double AverageClosingTime { get; set; }

    public override bool Equals(object obj)
    {
        var other = (RequestsPerMaintenanceStaffReport)obj;
        return MaintenanceWorker == other.MaintenanceWorker &&
               OpenRequests == other.OpenRequests &&
               AttendingRequests == other.AttendingRequests &&
               ClosedRequests == other.ClosedRequests;
    }
}
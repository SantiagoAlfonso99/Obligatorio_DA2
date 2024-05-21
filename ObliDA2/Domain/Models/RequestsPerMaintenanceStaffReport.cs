namespace Domain.Models;

public class RequestsPerMaintenanceStaffReport
{
    private const int MinValue = 0;
    private const double DoubleMinValue = 0;
    
    public RequestsPerMaintenanceStaffReport(){}
    public RequestsPerMaintenanceStaffReport(string workerName)
    {
        this.MaintenanceWorker = workerName;
        this.AttendingRequests = MinValue;
        this.ClosedRequests = MinValue;
        this.OpenRequests = MinValue;
        this.AverageClosingTime = DoubleMinValue;
    }
    
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
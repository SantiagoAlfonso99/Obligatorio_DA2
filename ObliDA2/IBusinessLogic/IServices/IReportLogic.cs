using Domain.Models;

namespace IBusinessLogic;

public interface IReportLogic
{
    public List<RequestsPerBuildingReport> CreateRequestsPerBuildingReports(string buildingName);

    public List<RequestsPerMaintenanceStaffReport> CreateRequestsPerMaintenanceStaffReports(string workerName,
        int buildingId);
}
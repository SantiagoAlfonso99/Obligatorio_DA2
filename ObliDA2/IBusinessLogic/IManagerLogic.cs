using Domain.Models;
namespace IBusinessLogic;

public interface IManagerLogic
{
    IEnumerable<Request> ViewRequests(string category = null);
    bool AssignRequestToMaintenance(int requestId, int maintenanceId);
    Request CreateRequest(string description, Apartment department, string category);
    Manager Create(Manager manager);
}
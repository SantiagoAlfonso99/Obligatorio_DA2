using Domain.Models;
namespace IBusinessLogic;

public interface IManagerLogic
{
    Manager Create(Manager manager);
    IEnumerable<Request> ViewRequests(string category = null);
    bool AssignRequestToMaintenance(int requestId, int maintenanceId);
    Request CreateRequest(string description, Apartment department, string category);
    IEnumerable<Request> GetAllRequest();
}
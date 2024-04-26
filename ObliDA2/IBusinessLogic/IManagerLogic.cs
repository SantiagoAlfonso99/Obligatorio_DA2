using Domain.Models;
namespace IBusinessLogic;

public interface IManagerLogic
{
    IEnumerable<Request> ViewRequests(string category = null);
    bool AssignRequestToMaintenance(int requestId, MaintenanceStaff worker);
    Request CreateRequest(string description, Apartment department, Category category);
    Manager Create(Manager manager);
}
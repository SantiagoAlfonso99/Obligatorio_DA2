using Domain.Models;
namespace IBusinessLogic;

public interface IManagerLogic
{
    IEnumerable<Request> ViewRequests(string category = null);
    bool AssignRequestToMaintenance(int requestId, MaintenanceStaff worker);
    Request CreateRequest(string description, Apartment department, Category category, Building building);
    Manager Create(Manager manager);
    Manager GetById(int id);
    IEnumerable<Request> GetAllRequest();
    Request MaintenanceStaffAcceptRequest(Request request, DateTime time);
    Request MaintenanceStaffCompleteRequest(Request request, int finalPrice, DateTime time);
}
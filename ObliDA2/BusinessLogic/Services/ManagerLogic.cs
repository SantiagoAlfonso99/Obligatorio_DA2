using Domain.Exceptions;
using BusinessLogic.IRepository;
using Domain.Models;
namespace BusinessLogic.Services;

public class ManagerLogic
{
    private IManagerRepository managerRepo;
    private IRequestRepository requestRepo;

    public ManagerLogic(IManagerRepository managerRepoIn, IRequestRepository requestRepoIn)
    {
        managerRepo = managerRepoIn;
        requestRepo = requestRepoIn;
    }

    public Manager Create(Manager manager)
    {
        managerRepo.Add(manager);
        return manager;
    }
    
    public IEnumerable<Request> ViewRequests(string category = null)
    {
        if (string.IsNullOrEmpty(category))
        {
            return requestRepo.GetAll();
        }
        return requestRepo.FilterByCategory(category);
    }

    public bool AssignRequestToMaintenance(int requestId, int maintenanceId)
    {
        if (!requestRepo.Exists(requestId) || !managerRepo.Exists(maintenanceId))
        {
            throw new InvalidRequestException();
        }
        Request request = requestRepo.Get(requestId);
        request.AssignedToMaintenanceId = maintenanceId;
        requestRepo.Update(request);
        return true;
    }

    public Request CreateRequest(string description, Apartment department, string category)
    {
        var newRequest = new Request()
        {
            Description = description,
            Department = department,
            Category = category,
            Status = RequestStatus.Open
        };
        requestRepo.Add(newRequest);
        return newRequest;
    }
    
    public IEnumerable<Request> GetAllRequest()
    {
        return requestRepo.GetAll();
    }
}

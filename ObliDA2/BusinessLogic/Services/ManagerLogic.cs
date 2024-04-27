using Domain.Exceptions;
using BusinessLogic.IRepository;
using Domain.Models;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class ManagerLogic : IManagerLogic
{
    private IManagerRepository managerRepo;
    private IRequestRepository requestRepo;

    public ManagerLogic(IManagerRepository managerRepoIn, IRequestRepository requestRepoIn)
    {
        managerRepo = managerRepoIn;
        requestRepo = requestRepoIn;
    }

    public Manager Create(Manager newManager)
    {
        managerRepo.Add(newManager);
        return newManager;
    }
    
    public IEnumerable<Request> ViewRequests(string category = null)
    {
        List<Request> requests = requestRepo.GetAll().ToList(); 
        if (string.IsNullOrEmpty(category))
        {
            return requests;
        }
        return requests.FindAll(request => request.Category.Name == category);
    }

    public bool AssignRequestToMaintenance(int requestId, MaintenanceStaff worker)
    {
        Request returnedRequest = requestRepo.Get(requestId);
        if (returnedRequest == null ||  returnedRequest.Status != RequestStatus.Open )
        {
            throw new InvalidRequestException();
        }
        Request request = requestRepo.Get(requestId);
        request.AssignedToMaintenanceId = worker.Id;
        request.AssignedToMaintenance = worker;
        requestRepo.Update(request);
        return true;
    }

    public Request CreateRequest(string description, Apartment department, Category category)
    {
        var newRequest = new Request()
        {
            Description = description,
            Department = department,
            Category = category,
            Status = RequestStatus.Open,
            AssignedToMaintenanceId = department.Building.Id,
            BuildingAssociated = department.Building
        };
        requestRepo.Add(newRequest);
        return newRequest;
    }

    public Manager GetById(int id)
    {
        Manager returnedManager = managerRepo.Get(id);
        if (returnedManager == null)
        {
            throw new NotFoundException();
        }
        return returnedManager;
    }
    
    public IEnumerable<Request> GetAllRequest()
    {
        return requestRepo.GetAll();
    }
}

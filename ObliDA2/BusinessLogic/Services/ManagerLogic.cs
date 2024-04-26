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

    public IEnumerable<Request> ViewRequests(string category = null)
    {
        List<Request> requests = requestRepo.GetAll().ToList(); 
        if (string.IsNullOrEmpty(category))
        {
            return requests;
        }
        return requests.FindAll(request => request.Category.Name == category);
    }

    public bool AssignRequestToMaintenance(int requestId, int maintenanceId)
    {
        Request returnedRequest = requestRepo.Get(requestId);
        if (returnedRequest == null || !managerRepo.Exists(maintenanceId) || returnedRequest.Status != RequestStatus.Open )
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
            Category = new Category(){Name = category},
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

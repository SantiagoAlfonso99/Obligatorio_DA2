using Domain.Models;
namespace BusinessLogic.IRepository;

public interface IRequestRepository
{
    void Add(Request request);
    Request Get(int requestId);
    bool Exists(int requestId);
    void Update(Request request);
    void Remove(Request request);
    IEnumerable<Request> GetAll();
    IEnumerable<Request> FilterByCategory(string category);
}

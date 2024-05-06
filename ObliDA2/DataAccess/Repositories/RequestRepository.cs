using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Models;
using BusinessLogic.IRepository;

namespace DataAccess.Repositories;

public class RequestRepository : IRequestRepository
{
    protected AppContext Context { get; set; }
    
    public RequestRepository(AppContext context)
    {
        Context = context;
    }
    
    public void Add(Request request)
    {
        Context.Requests.Add(request);
        Context.SaveChanges();
    }
    
    public IEnumerable<Request> GetAll()
    {
        return Context.Requests.Include(request => request.Department).ToList();
    }
    
    public Request Get(int id)
    {
        return Context.Requests
            .Include(request => request.Department).ThenInclude(apartment => apartment.Building)
            .FirstOrDefault(request => request.Id == id);
    }
    
    public void Remove(Request request)
    {
        Context.Set<Request>().Remove(request);
        Context.SaveChanges();
    }

    public void Update(Request request)
    {
        Context.Entry(request).State = EntityState.Modified;
        Context.SaveChanges();
    }
    
    public bool Exists(int id)
    {
        return Context.Requests.Any(request => request.Id == id );
    }
}
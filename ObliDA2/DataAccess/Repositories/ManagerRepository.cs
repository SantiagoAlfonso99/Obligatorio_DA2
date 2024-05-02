using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Models;
using BusinessLogic.IRepository;

namespace DataAccess.Repositories;

public class ManagerRepository : IManagerRepository
{
    protected AppContext Context { get; set; }
    
    public ManagerRepository(AppContext context)
    {
        Context = context;
    }
    
    public void Add(Manager manager)
    {
        Context.Managers.Add(manager);
        Context.SaveChanges();
    }
    
    public IEnumerable<Manager> GetAll()
    {
        return Context.Managers.ToList();
    }
    
    public Manager Get(int id)
    {
        return Context.Managers.Find(id);
    }
    
    public void Remove(Manager manager)
    {
        Context.Set<Manager>().Remove(manager);
        Context.SaveChanges();
    }
    
    public bool Exists(int id)
    {
        return Context.Managers.Any(manager => manager.Id == id );
    }
    
    public void Update(Manager manager)
    {
        Context.Entry(manager).State = EntityState.Modified;
        Context.SaveChanges();
    }
}
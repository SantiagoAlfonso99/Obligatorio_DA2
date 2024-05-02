using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Models;
using BusinessLogic.IRepository;

namespace DataAccess.Repositories;

public class AdminRepository : IAdminRepository
{
    protected AppContext Context { get; set; }
    
    public AdminRepository(AppContext context)
    {
        Context = context;
    }
    
    public void Add(Admin admin)
    {
        Context.Admins.Add(admin);
        Context.SaveChanges();
    }
    
    public List<Admin> GetAll()
    {
        return Context.Admins.ToList();
    }
    
    public Admin Get(int id)
    {
        return Context.Admins.Find(id);
    }
    
    public void Remove(Admin admin)
    {
        Context.Set<Admin>().Remove(admin);
        Context.SaveChanges();
    }
    
    public void Update(Admin admin)
    {
        Context.Entry(admin).State = EntityState.Modified;
        Context.SaveChanges();
    }
}
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Models;
using BusinessLogic.IRepository;

namespace DataAccess.Repositories;

public class MaintenanceStaffRepository : IMaintenanceStaffRepository
{
    protected AppContext Context { get; set; }
    
    public MaintenanceStaffRepository(AppContext context)
    {
        Context = context;
    }
    
    public void Create(MaintenanceStaff staff)
    {
        Context.MaintenancePersonnel.Add(staff);
        Context.SaveChanges();
    }
    
    public List<MaintenanceStaff> GetAll()
    {
        return Context.MaintenancePersonnel.Include(worker => worker.Buildings).ToList();
    }
    
    public MaintenanceStaff GetById(int id)
    {
        return Context.MaintenancePersonnel.Find(id);
    }
    
    public void Delete(MaintenanceStaff staff)
    {
        Context.Set<MaintenanceStaff>().Remove(staff);
        Context.SaveChanges();
    }
}
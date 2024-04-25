using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Models;
using BusinessLogic.IRepository;

namespace DataAccess.Repositories;

public class BuildingRepository : IBuildingRepository
{
    protected AppContext Context { get; set; }
    
    public BuildingRepository(AppContext context)
    {
        Context = context;
    }
    
    public void Create(Building building)
    {
        Context.Buildings.Add(building);
        Context.SaveChanges();
    }
    
    public List<Building> GetAll()
    {
        return Context.Buildings.ToList();
    }
    
    public Building GetById(int id)
    {
        return Context.Buildings.Find(id);
    }
    
    public void Delete(Building building)
    {
        Context.Set<Building>().Remove(building);
        Context.SaveChanges();
    }
    
    public void Update(Building building)
    {
        Context.Entry(building).State = EntityState.Modified;
        Context.SaveChanges();
    }
}
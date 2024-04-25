using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Models;
using BusinessLogic.IRepository;

namespace DataAccess.Repositories;


public class CategoryRepository : ICategoryRepository
{
    protected AppContext Context { get; set; }
    
    public CategoryRepository(AppContext context)
    {
        Context = context;
    }
    
    public void Create(Category category)
    {
        Context.Categories.Add(category);
        Context.SaveChanges();
    }
    
    public List<Category> GetAll()
    {
        return Context.Categories.ToList();
    }
    
    public Category GetById(int id)
    {
        return Context.Categories.Find(id);
    }
    
    public void Delete(Category category)
    {
        Context.Set<Category>().Remove(category);
        Context.SaveChanges();
    }
}
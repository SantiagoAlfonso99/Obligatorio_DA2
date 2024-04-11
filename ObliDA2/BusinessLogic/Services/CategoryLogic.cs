using Domain.Exceptions;
using BusinessLogic.IRepository;
using Domain.Models;

namespace BusinessLogic.Services;

public class CategoryLogic
{
    public ICategoryRepository categoryRepo;

    public CategoryLogic(ICategoryRepository categoryRepoIn)
    {
        categoryRepo = categoryRepoIn;
    }
    
    public Category GetById(int id)
    {
        return categoryRepo.GetById(id);
    }
    
    public List<Category> GetAll()
    {
        return categoryRepo.GetAll();
    }
    
    public Category Create(Category newCategory)
    { 
        categoryRepo.Create(newCategory);
        return newCategory;
    }
    
    public bool Delete(int id)
    {
        Category categoryToRemove = categoryRepo.GetById(id);
        if (categoryToRemove == null)
        {
            return false;
        }
        categoryRepo.Delete(categoryToRemove);
        return true;
    }
}
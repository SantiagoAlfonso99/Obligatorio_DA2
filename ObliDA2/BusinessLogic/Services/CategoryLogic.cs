using Domain.Exceptions;
using BusinessLogic.IRepository;
using Domain.Models;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class CategoryLogic : ICategoryLogic
{
    public ICategoryRepository categoryRepo;

    public CategoryLogic(ICategoryRepository categoryRepoIn)
    {
        categoryRepo = categoryRepoIn;
    }
    
    public Category GetById(int id)
    {
        Category returnedCategory = categoryRepo.GetById(id);
        if (returnedCategory == null)
        {
            throw new NotFoundException();
        }
        return returnedCategory;
    }
    
    public List<Category> GetAll()
    {
        return categoryRepo.GetAll();
    }
    
    public Category Create(Category newCategory)
    {
        List<Category> categories = categoryRepo.GetAll();
        bool success = categories.Exists(category => category.Name == newCategory.Name);
        if (success)
        {
            throw new DuplicateEntryException();
        }
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
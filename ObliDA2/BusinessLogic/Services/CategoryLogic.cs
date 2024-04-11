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
}
using Domain.Models;

namespace BusinessLogic.IRepository;

public interface ICategoryRepository
{
    Category GetById(int id);
    List<Category> GetAll();
    void Create(Category newCategory);
    void Delete(Category categoryToRemove);
}
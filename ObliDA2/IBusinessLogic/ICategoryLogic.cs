using Domain.Models;
namespace IBusinessLogic;

public interface ICategoryLogic
{
    List<Category> GetAll();
    Category GetById(int id);
    Category Create(string name);
    bool Delete(int id);
}
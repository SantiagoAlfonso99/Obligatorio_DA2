using Domain.Models;
namespace IBusinessLogic;

public interface ICategoryLogic
{
    List<Category> GetAll();
}
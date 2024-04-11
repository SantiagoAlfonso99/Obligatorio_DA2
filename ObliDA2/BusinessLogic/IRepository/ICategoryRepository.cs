using Domain.Models;

namespace BusinessLogic.IRepository;

public interface ICategoryRepository
{
    Category GetById(int id);
}
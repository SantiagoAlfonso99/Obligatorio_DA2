using Domain.Models;

namespace BusinessLogic.IRepository;

public interface IBuildingRepository
{
    List<Building> GetAll();
}
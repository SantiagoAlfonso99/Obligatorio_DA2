using Domain.Models;

namespace BusinessLogic.IRepository;

public interface IBuildingRepository
{
    List<Building> GetAll();
    Building GetById(int id);
    void Create(Building newBuilding);
    void Delete(Building buildingToRemove);
    void Update(Building buildingToUpdate);
}
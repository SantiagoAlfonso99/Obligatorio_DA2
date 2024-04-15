using Domain.Models;

namespace IBusinessLogic;

public interface IBuildingLogic
{
    List<Building> GetAll();
    Building GetById(int id);
    Building Create(Building newBuilding);
    bool Delete(Building buildingToRemove);
    Building Update(int id, Building newAttributes);
}
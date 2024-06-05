using Domain.Models;

namespace IBusinessLogic;

public interface IBuildingLogic
{
    List<Building> GetAll();
    Building GetById(int id);
    Building Create(Building newBuilding);
    bool Delete(int id, int managerId);
    Building Update(int id, Building newAttributes, int managerId);
    Building UpdateManager(Building newAttributes);
}
using Domain.Models;

namespace IBusinessLogic;

public interface IBuildingLogic
{
    List<Building> GetAll();
    Building GetById(int id);
}
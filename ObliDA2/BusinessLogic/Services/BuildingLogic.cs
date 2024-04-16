using BusinessLogic.IRepository;
using Domain.Models;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class BuildingLogic
{
    private IBuildingRepository buildingRepo;

    public BuildingLogic(IBuildingRepository buildingRepoIn)
    {
        buildingRepo = buildingRepoIn;
    }
    
    public List<Building> GetAll()
    {
        return buildingRepo.GetAll();
    }

    public Building GetById(int id)
    {
        return buildingRepo.GetById(id);
    }
    
    public Building Create(Building newBuilding)
    { 
        buildingRepo.Create(newBuilding);
        return newBuilding;
    }
}
using BusinessLogic.IRepository;
using Domain.Models;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class BuildingLogic : IBuildingLogic
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
    
    public bool Delete(int id)
    {
        Building returnedBuilding = buildingRepo.GetById(id);
        if (returnedBuilding == null)
        {
            return false;
        }
        buildingRepo.Delete(returnedBuilding);
        return true;
    }
    
    public Building Update(int id,Building newAttributes)
    {
        Building returnedBuilding = buildingRepo.GetById(id);
        if (returnedBuilding == null)
        {
            throw new ArgumentException();
        }
        returnedBuilding.CommonExpenses = newAttributes.CommonExpenses;
        returnedBuilding.ConstructionCompany = newAttributes.ConstructionCompany;
        buildingRepo.Update(returnedBuilding);
        return returnedBuilding;
    }
}
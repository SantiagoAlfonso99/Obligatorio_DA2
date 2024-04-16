using BusinessLogic.IRepository;
using Domain.Exceptions;
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
        Building returnedBuilding =  buildingRepo.GetById(id);
        if (returnedBuilding == null)
        {
            throw new NotFoundException();
        }
        return returnedBuilding;
    }
    
    public Building Create(Building newBuilding)
    {
        List<Building> buildings = buildingRepo.GetAll();
        bool InvalidInputs = buildings.Exists(building => (building.Name == newBuilding.Name || building.Address == newBuilding.Address
            || (building.Longitude == newBuilding.Longitude && building.Latitude == newBuilding.Latitude)));
        if (InvalidInputs)
        {
            throw new DuplicateEntryException();
        }
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
            throw new NotFoundException();
        }
        returnedBuilding.CommonExpenses = newAttributes.CommonExpenses;
        returnedBuilding.ConstructionCompany = newAttributes.ConstructionCompany;
        buildingRepo.Update(returnedBuilding);
        return returnedBuilding;
    }
}
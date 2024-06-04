using System.Reflection;
using BusinessLogic.IRepository;
using Domain.Exceptions;
using IImporter;
using IBusinessLogic;
using Domain.Models;

namespace BusinessLogic.Services;

public class ImporterLogic : IImporterLogic
{
    private readonly IApartmentRepository apartmentRepo;
    private readonly IBuildingRepository buildingRepository;
    private readonly IApartmentOwnerRepository ownerRepository;
    private readonly IManagerRepository managerRepository;
    
    public ImporterLogic(IBuildingRepository buildingRepoIn, IApartmentRepository apartmentRepoIn,
        IManagerRepository managerRepoIn, IApartmentOwnerRepository ownerRepoIn)
    {
        buildingRepository = buildingRepoIn;
        apartmentRepo = apartmentRepoIn;
        ownerRepository = ownerRepoIn;
        managerRepository = managerRepoIn;
    }
    
    public List<ImporterInterface> GetAllImporters()
    {
        var importersPath = "./Importers"; 
        string[] filePaths = Directory.GetFiles(importersPath);
        List<ImporterInterface> availableImporters = new List<ImporterInterface>();

        foreach (string file in filePaths)
        {
            if (FileIsDll(file))
            {
                FileInfo dllFile  = new FileInfo(file);
                Assembly myAssembly = Assembly.LoadFile(dllFile.FullName);

                foreach (Type type in myAssembly.GetTypes())
                {
                    if (ImplementsRequiredInterface(type))
                    {
                        ImporterInterface instance = (ImporterInterface)Activator.CreateInstance(type);
                        availableImporters.Add(instance);
                    }
                }
            }
        }

        return availableImporters;
    }

    public List<Building> ImportBuildings(string importerName, ConstructionCompany company)
    {
        var importers = GetAllImporters();
        var selectedImporter = importers.Find(importer => importer.GetName() == importerName);
        if (selectedImporter != null)
        {
            List<BuildingImporterDTO> returnedBuildingsDto = selectedImporter.ImportBuildings();
            return ReturnBuildings(returnedBuildingsDto, company);
        }
        throw new NotFoundException();
    }

    public List<Building> ReturnBuildings(List<BuildingImporterDTO> buildingsDto, ConstructionCompany company)
    {
        List<Building> newBuildings = new List<Building>();
        foreach (var buildingDto in buildingsDto)
        {
            Manager newManager = buildingDto.Manager.ToEntity();
            try
            {
                managerRepository.Add(newManager);
            }
            catch (DuplicateEntryException ex)
            {
                continue;
            }
            Building newBuilding = buildingDto.ToEntity(company);
            newBuilding.BuildingManager = newManager;
            buildingRepository.Create(newBuilding);
            newBuildings.Add(newBuilding);
            foreach (var apartmentDto in buildingDto.Apartments)
            {
                ApartmentOwner newOwner = apartmentDto.Owner.ToEntity();
                try
                {
                    ownerRepository.Create(newOwner);
                }
                catch (DuplicateEntryException ex)
                {
                    continue;
                }
                Apartment newApartment = apartmentDto.ToEntity();
                newApartment.Owner = newOwner;
                newApartment.Building = newBuilding;
                apartmentRepo.Create(newApartment);
            }
        }
        return newBuildings;
    }
    
    private bool FileIsDll(string file)
    {
        return file.EndsWith("dll");
    }
    
    private bool ImplementsRequiredInterface(Type type)
    {
        return typeof(ImporterInterface).IsAssignableFrom(type) && !type.IsInterface;
    }
}
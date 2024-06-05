using IImporter;
using Domain.Models;

namespace IBusinessLogic;

public interface IImporterLogic
{
    List<ImporterInterface> GetAllImporters();
    List<Building> ImportBuildings(string importerName, ConstructionCompany company);
}
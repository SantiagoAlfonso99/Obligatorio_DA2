
namespace IImporter;

public interface ImporterInterface
{
    string GetName();
    List<BuildingImporterDTO> ImportBuildings();
}
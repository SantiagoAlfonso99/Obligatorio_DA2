using Domain.Models;

namespace IImporter;

public interface ImporterInterface
{
    string GetName();
    List<Building> ImportBuildings();
}
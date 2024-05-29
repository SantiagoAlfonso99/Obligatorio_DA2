using System.Reflection;
using IImporter;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class ImporterLogic : IImporterLogic
{
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
    
    
    private bool FileIsDll(string file)
    {
        return file.EndsWith("dll");
    }
    
    public bool ImplementsRequiredInterface(Type type)
    {
        return typeof(ImporterInterface).IsAssignableFrom(type) && !type.IsInterface;
    }
}
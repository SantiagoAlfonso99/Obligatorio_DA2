using Domain.Models;

namespace IImporter;

public class OwnerImporterDTO
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    public ApartmentOwner ToEntity()
    {
        return new ApartmentOwner()
        {
            Name = this.Name,
            LastName = this.LastName,
            Email = this.Email,
        };
    }
}
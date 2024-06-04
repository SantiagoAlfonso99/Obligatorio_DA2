using Domain.Models;

namespace IImporter;

public class ApartmentImporterDTO
{
    public int Floor { get; set; }
    public int Number { get; set; }
    public int NumberOfBedrooms { get; set; }
    public int NumberOfBathroom { get; set; }
    public bool Terrace { get; set; }
    public virtual OwnerImporterDTO Owner { get; set; } 
    
    public Apartment ToEntity()
    {
        return new Apartment()
        {
            Floor = this.Floor,
            Number = this.Number,
            NumberOfBedrooms = this.NumberOfBedrooms,
            NumberOfBathrooms = this.NumberOfBathroom,
            Terrace = this.Terrace
        };
    }
}
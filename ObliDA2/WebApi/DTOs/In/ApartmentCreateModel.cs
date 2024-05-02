using Domain.Models;

namespace WebApi.DTOs.In;

public class ApartmentCreateModel
{
    public int Floor { get; set; }
    public int Number { get; set; }
    public int NumberOfBedrooms { get; set; }
    public int NumberOfBathrooms { get; set; }
    public bool Terrace { get; set; }
    public int OwnerId { get; set; }
    public int BuildingId { get; set; }

    public Apartment ToEntity()
    {
        return new Apartment()
        {
            Floor = this.Floor,
            Number = this.Number,
            NumberOfBathrooms = this.NumberOfBathrooms,
            NumberOfBedrooms = this.NumberOfBedrooms,
            Terrace = this.Terrace
        };
    }
}
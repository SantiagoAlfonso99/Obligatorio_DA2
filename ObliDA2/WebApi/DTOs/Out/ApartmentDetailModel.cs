using System.Runtime.InteropServices.JavaScript;
using Domain.Models;

namespace WebApi.DTOs.Out;

public class ApartmentDetailModel
{
    public ApartmentDetailModel(Apartment apartmentIn)
    {
        Id = apartmentIn.Id;
        Floor = apartmentIn.Floor;
        Number = apartmentIn.Number;
        NumberOfBedrooms = apartmentIn.NumberOfBedrooms;
        NumberOfBathrooms = apartmentIn.NumberOfBathrooms;
        Terrace = apartmentIn.Terrace;
        Owner = apartmentIn.Owner;
        Building = apartmentIn.Building;
    }
    
    public int Id { get; set; }
    public int Floor { get; set; }
    public int Number { get; set; }
    public int NumberOfBedrooms { get; set; }
    public int NumberOfBathrooms { get; set; }
    public bool Terrace { get; set; }
    public ApartmentOwner Owner { get; set; }
    public Building Building { get; set; }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        ApartmentDetailModel other = (ApartmentDetailModel)obj;
        return Id == other.Id
               && Floor == other.Floor
               && Number == other.Number
               && NumberOfBedrooms == other.NumberOfBedrooms
               && NumberOfBathrooms == other.NumberOfBathrooms
               && Terrace == other.Terrace
               && Owner == other.Owner
               && Building == other.Building;
    }
}
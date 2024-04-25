namespace Domain.Models;

public class Apartment
{
    private const int MinValue = 0;
    
    public int numberOfBedrooms;
    public int numberOfBathrooms;
    
    public int Id { get; set; }
    public int Floor { get; set; }
    public int Number { get; set; }
    
    
    public int NumberOfBedrooms
    {
        get => numberOfBedrooms;
        set
        {
            if (value < MinValue)
            {
                throw new ArgumentException();
            }
            numberOfBedrooms = value;
        }
    }
    
    public int NumberOfBathrooms
    {
        get => numberOfBathrooms;
        set
        {
            if (value < MinValue)
            {
                throw new ArgumentException();
            }
            numberOfBathrooms = value;
        }
    }
    
    public bool Terrace { get; set; }
    public virtual ApartmentOwner Owner { get; set; } 
    public virtual Building Building { get; set; }

    public virtual ICollection<Request> Requests { get; set; }
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Apartment other = (Apartment)obj;
        return Id == other.Id
               && Floor == other.Floor
               && Number == other.Number
               && NumberOfBedrooms == other.NumberOfBedrooms
               && NumberOfBathrooms == other.NumberOfBathrooms
               && Terrace == other.Terrace
               && Owner.Equals(other.Owner)
               && Building.Equals(other.Building);
    }
}
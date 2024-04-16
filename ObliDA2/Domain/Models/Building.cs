
namespace Domain.Models;

public class Building
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string ConstructionCompany { get; set; }
    public int CommonExpenses { get; set; }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Building other = (Building)obj;
        return Id == other.Id
               && Name == other.Name
               && Address == other.Address
               && Latitude.Equals(other.Latitude)
               && Longitude.Equals(other.Longitude)
               && ConstructionCompany == other.ConstructionCompany
               && CommonExpenses == other.CommonExpenses;
    }
}
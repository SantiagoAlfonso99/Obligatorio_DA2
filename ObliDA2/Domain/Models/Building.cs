using Domain.Exceptions;

namespace Domain.Models;

public class Building
{
    private string name;
    private string address;
    private string constructionCompany;
    
    public int Id { get; set; }
    public string Name
    {
        get => name;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new EmptyOrNullException();
            }
            name = value;
        }
    }
    public string Address
    {
        get => address;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new EmptyOrNullException();
            }
            address = value;
        }
    }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string ConstructionCompany
    {
        get => constructionCompany;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new EmptyOrNullException();
            }
            constructionCompany = value;
        }
    }
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
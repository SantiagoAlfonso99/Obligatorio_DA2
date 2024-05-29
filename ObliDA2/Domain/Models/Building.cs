using System.Text.Json.Serialization;
using Domain.Exceptions;

namespace Domain.Models;

public class Building
{
    public string name;
    public string address;
    public string constructionCompany;
    public int commonExpenses;

    private const int MinValueExpenses = 0;
    
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
    public int CompanyId { get; set; }
    public virtual ConstructionCompany Company { get; set; }
    public int CommonExpenses
    {
        get => commonExpenses;
        set
        {
            if (value < MinValueExpenses)
            {
                throw new ArgumentException();
            }
            commonExpenses = value;
        }
    }
    public int? BuildingManagerId { get; set; }
    public virtual Manager? BuildingManager { get; set; }
    [JsonIgnore]
    public virtual ICollection<MaintenanceStaff> Workers {get; set; }
    
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
            && CommonExpenses == other.CommonExpenses
            && BuildingManager.Equals(other.BuildingManager);
    }
}
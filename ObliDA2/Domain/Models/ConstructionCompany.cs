using Domain.Exceptions;
namespace Domain.Models;

public class ConstructionCompany
{
    public int Id { get; set; }
    public string name;
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
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        ConstructionCompany other = (ConstructionCompany)obj;
        return Id == other.Id
               && Name == other.Name;
    }
}
using Domain.Exceptions;

namespace Domain.Models;

public class Category
{
    public string name;
    
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
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        Category other = (Category)obj;
        return Id == other.Id
               && Name == other.Name;
    }
}
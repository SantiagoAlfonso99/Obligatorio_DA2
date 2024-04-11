namespace Domain.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    
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
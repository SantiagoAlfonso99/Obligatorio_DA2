using Domain.Models;

namespace WebApi.DTOs.Out;

public class ApartmentOwnerDetailModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public ApartmentOwnerDetailModel(ApartmentOwner owner)
    {
        Id = owner.Id;
        Name = owner.Name;
        LastName = owner.LastName;
        Email = owner.Email;
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        ApartmentOwnerDetailModel other = (ApartmentOwnerDetailModel)obj;
        return Id == other.Id
               && Name == other.Name
               && LastName == other.LastName
               && Email == other.Email;
    }
}
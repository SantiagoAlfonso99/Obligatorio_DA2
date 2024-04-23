using Domain.Models;

namespace WebApi.DTOs.In;

public class ApartmentOwnerCreateModel
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public ApartmentOwner ToEntity()
    {
        return new ApartmentOwner()
        {
            Name = this.Name,
            LastName = this.LastName,
            Email = this.Email
        };
    }
}
using Domain.Models;

namespace BusinessLogic.IRepository;

public interface IApartmentOwnerRepository
{
    void Create(ApartmentOwner owner);
}
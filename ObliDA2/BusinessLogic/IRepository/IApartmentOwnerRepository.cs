using Domain.Models;

namespace BusinessLogic.IRepository;

public interface IApartmentOwnerRepository
{
    void Create(ApartmentOwner owner);
    ApartmentOwner GetById(int id);
    void Delete(ApartmentOwner owner);
    void Update(ApartmentOwner owner);
}
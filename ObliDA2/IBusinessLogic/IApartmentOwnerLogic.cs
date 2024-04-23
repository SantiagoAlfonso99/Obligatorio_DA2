using Domain.Models;

namespace IBusinessLogic;

public interface IApartmentOwnerLogic
{
    ApartmentOwner GetById(int id);
    ApartmentOwner Create(ApartmentOwner newOwner);
    ApartmentOwner Update(int id, ApartmentOwner newAttributes);
    bool Delete(int id);
}
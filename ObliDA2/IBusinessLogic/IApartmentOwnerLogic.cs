using Domain.Models;

namespace IBusinessLogic;

public interface IApartmentOwnerLogic
{
    ApartmentOwner GetById(int id);
}
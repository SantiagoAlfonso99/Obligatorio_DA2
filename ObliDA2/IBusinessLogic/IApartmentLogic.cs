using Domain.Models;

namespace IBusinessLogic;

public interface IApartmentLogic
{
    List<Apartment> GetAll();
    Apartment GetById(int id);
    Apartment Create(Apartment newApartment);
    bool Delete(int id);
}
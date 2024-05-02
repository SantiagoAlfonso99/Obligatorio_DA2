using Domain.Models;

namespace BusinessLogic.IRepository;

public interface IApartmentRepository
{
    void Create(Apartment newApartment);
    List<Apartment> GetAll();
    Apartment GetById(int id);
    void Delete(Apartment apartmentToRemove);
}
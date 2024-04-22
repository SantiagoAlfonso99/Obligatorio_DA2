using Domain.Models;

namespace BusinessLogic.IRepository;

public interface IApartmentRepository
{
    void Create(Apartment newApartment);
}
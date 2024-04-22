using BusinessLogic.IRepository;
using Domain.Models;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class ApartmentLogic
{
    private readonly IApartmentRepository apartmentRepo;

    public ApartmentLogic(IApartmentRepository apartmentRepoIn)
    {
        apartmentRepo = apartmentRepoIn;
    }
    
    public Apartment Create(Apartment newApartment)
    {
        apartmentRepo.Create(newApartment);
        return newApartment;
    }
    
    public List<Apartment> GetAll()
    {
        return apartmentRepo.GetAll();
    }

    public Apartment GetById(int id)
    {
        return apartmentRepo.GetById(id);
    }
    
    public bool Delete(int id)
    {
        Apartment returnedApartment = apartmentRepo.GetById(id);
        if (returnedApartment != null)
        {
            apartmentRepo.Delete(returnedApartment);
            return true;
        }
        return false;
    }
}
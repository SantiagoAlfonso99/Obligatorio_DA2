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
}
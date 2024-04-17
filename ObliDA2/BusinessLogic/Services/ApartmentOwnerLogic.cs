using BusinessLogic.IRepository;
using Domain.Models;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class ApartmentOwnerLogic
{
    private IApartmentOwnerRepository ownerRepository;

    public ApartmentOwnerLogic(IApartmentOwnerRepository repoIn)
    {
        ownerRepository = repoIn;
    }
    
    public ApartmentOwner Create(ApartmentOwner newOwner)
    {
        ownerRepository.Create(newOwner);
        return newOwner;
    }   
}
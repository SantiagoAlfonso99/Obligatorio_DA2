using BusinessLogic.IRepository;
using Domain.Exceptions;
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
    
    public ApartmentOwner GetById(int id)
    {
        ApartmentOwner returnedOwner = ownerRepository.GetById(id);
        if (returnedOwner == null)
        {
            throw new NotFoundException();
        }
        return returnedOwner;
    }
    
    public bool Delete(int id)
    {
        ApartmentOwner returnedOwner = ownerRepository.GetById(id);
        if (returnedOwner == null)
        {
            return false;
        }
        ownerRepository.Delete(returnedOwner);
        return true;
    }
    
    public ApartmentOwner Update(int id, ApartmentOwner newAttributes)
    {
        ApartmentOwner returnedOwner = ownerRepository.GetById(id);
        if (returnedOwner == null)
        {
            throw new NotFoundException();
        }
        returnedOwner.Name = newAttributes.Name;
        returnedOwner.LastName = newAttributes.LastName;
        returnedOwner.Email = newAttributes.Email;
        ownerRepository.Update(returnedOwner);
        return returnedOwner;
    }
}
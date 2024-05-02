using BusinessLogic.IRepository;
using Domain.Exceptions;
using Domain.Models;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class ApartmentOwnerLogic : IApartmentOwnerLogic
{
    private IApartmentOwnerRepository ownerRepository;

    public ApartmentOwnerLogic(IApartmentOwnerRepository repoIn)
    {
        ownerRepository = repoIn;
    }
    
    public ApartmentOwner Create(ApartmentOwner newOwner)
    {
        List<ApartmentOwner> owners = ownerRepository.GetAll();
        bool success = owners.Exists(owner => owner.Email == newOwner.Email);
        if (success)
        {
            throw new DuplicateEntryException();
        }
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
        List<ApartmentOwner> owners = ownerRepository.GetAll();
        bool success = owners.Exists(owner => (owner.Email == newAttributes.Email && owner.Id != returnedOwner.Id));
        if (success)
        {
            throw new DuplicateEntryException();
        }
        returnedOwner.Name = newAttributes.Name;
        returnedOwner.LastName = newAttributes.LastName;
        returnedOwner.Email = newAttributes.Email;
        ownerRepository.Update(returnedOwner);
        return returnedOwner;
    }
}
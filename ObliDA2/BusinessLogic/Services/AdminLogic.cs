using BusinessLogic.Exceptions;
using BusinessLogic.IRepository;
using BusinessLogic.Models;
namespace BusinessLogic.Services;

public class AdminLogic
{
    private IAdminRepository adminRepo;

    public AdminLogic(IAdminRepository adminRepoIn)
    {
        adminRepo = adminRepoIn;
    }
    public Admin Create(Admin adminIn)
    {
        adminRepo.Add(adminIn);
        return adminIn;
    }

    public Admin GetById(int id)
    {
        if (!adminRepo.Exists(id))
        {
            throw new InvalidAdminException();
        }
        return adminRepo.Get(id);
    }
    
    public int Count()
    {
        return adminRepo.Count();
    }
    
    public Admin Update(int id, Admin updatedAdmin)
    {
        if (!adminRepo.Exists(id))
        {
            throw new InvalidAdminException();
        }
        Admin returnedAdmin = adminRepo.Get(id);
        returnedAdmin.Name = updatedAdmin.Name;
        returnedAdmin.Password = updatedAdmin.Password;
        adminRepo.Update(returnedAdmin);
        return returnedAdmin;
    }
    
    public bool Delete(int id)
    {
        if (!adminRepo.Exists(id))
        {
            throw new InvalidAdminException();
        }
        Admin returnedAdmin = adminRepo.Get(id);
        adminRepo.Remove(returnedAdmin);
        return true;
    }
}
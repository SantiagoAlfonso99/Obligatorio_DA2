using Domain.Exceptions;
using BusinessLogic.IRepository;
using Domain.Models;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class AdminLogic : IAdminLogic
{
    private IAdminRepository adminRepo;

    public AdminLogic(IAdminRepository adminRepoIn)
    {
        adminRepo = adminRepoIn;
    }

    public List<Admin> GetAll()
    {
        return adminRepo.GetAll();
    }
    
    public Admin Create(Admin adminIn)
    {
        adminRepo.Add(adminIn);
        return adminIn;
    }

    public Admin GetById(int id)
    {
        Admin returnedAdmin = adminRepo.Get(id);
        if (returnedAdmin == null)
        {
            throw new NotFoundException();
        }
        return returnedAdmin;
    }
    
    public Admin Update(int id, Admin updatedAdmin)
    {
        Admin returnedAdmin = this.GetById(id);
        returnedAdmin.Name = updatedAdmin.Name;
        returnedAdmin.Password = updatedAdmin.Password;
        returnedAdmin.LastName = updatedAdmin.LastName;
        adminRepo.Update(returnedAdmin);
        return returnedAdmin;
    }
    
    public bool Delete(int id)
    {
        Admin returnedAdmin = adminRepo.Get(id);
        if (returnedAdmin == null)
        {
            return false;
        }
        adminRepo.Remove(returnedAdmin);
        return true;
    }
}
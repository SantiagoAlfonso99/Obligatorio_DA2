using Domain.Exceptions;
using BusinessLogic.IRepository;
using Domain.Models;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class MaintenanceStaffLogic
{
    private IMaintenanceStaffRepository staffRepo;

    public MaintenanceStaffLogic(IMaintenanceStaffRepository staffRepoIn)
    {
        staffRepo = staffRepoIn;
    }

    public List<MaintenanceStaff> GetAll()
    {
        return staffRepo.GetAll();
    }

    public MaintenanceStaff GetById(int id)
    {
        return staffRepo.GetById(id);
    }
    
    public MaintenanceStaff Create(MaintenanceStaff newStaff)
    {
        staffRepo.Create(newStaff);
        return newStaff;
    }
    
    public bool Delete(int id)
    {
        MaintenanceStaff staffToRemove = staffRepo.GetById(id);
        if (staffToRemove == null)
        {
            return false;
        }
        staffRepo.Delete(staffToRemove);
        return true;
    }
}
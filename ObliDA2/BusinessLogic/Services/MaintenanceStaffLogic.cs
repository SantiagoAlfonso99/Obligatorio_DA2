using Domain.Exceptions;
using BusinessLogic.IRepository;
using Domain.Models;
using IBusinessLogic;

namespace BusinessLogic.Services;

public class MaintenanceStaffLogic : IMaintenanceLogic
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
        MaintenanceStaff returnedStaff = staffRepo.GetById(id);
        if (returnedStaff == null)
        {
            throw new NotFoundException();
        }
        return returnedStaff;
    }
    
    public MaintenanceStaff Create(MaintenanceStaff newStaff)
    {
        List<MaintenanceStaff> allStaff = staffRepo.GetAll();
        bool success = allStaff.Exists(staff => staff.Email == newStaff.Email);
        if (success)
        {
            throw new NotFoundException();
        }
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
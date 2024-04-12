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
    
    
}
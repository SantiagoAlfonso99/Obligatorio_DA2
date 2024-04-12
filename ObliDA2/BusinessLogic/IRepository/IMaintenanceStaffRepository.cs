using Domain.Models;

namespace BusinessLogic.IRepository;

public interface IMaintenanceStaffRepository
{
    List<MaintenanceStaff> GetAll();
}
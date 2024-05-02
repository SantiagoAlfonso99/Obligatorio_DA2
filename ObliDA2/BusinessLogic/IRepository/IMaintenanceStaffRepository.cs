using Domain.Models;

namespace BusinessLogic.IRepository;

public interface IMaintenanceStaffRepository
{
    List<MaintenanceStaff> GetAll();
    MaintenanceStaff GetById(int id);
    void Create(MaintenanceStaff newStaff);
    void Delete(MaintenanceStaff staffToRemove);
}
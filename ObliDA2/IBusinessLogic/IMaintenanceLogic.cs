using Domain.Models;
namespace IBusinessLogic;

public interface IMaintenanceLogic
{
    public List<MaintenanceStaff> GetAll();
    public MaintenanceStaff GetById(int id);
    public bool Delete(int id);
    public MaintenanceStaff Create(MaintenanceStaff newStaff);
}
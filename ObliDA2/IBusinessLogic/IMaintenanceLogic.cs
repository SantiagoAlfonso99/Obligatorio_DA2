using Domain.Models;
namespace IBusinessLogic;

public interface IMaintenanceLogic
{
    public List<MaintenanceStaff> GetAll();
}
using Domain.Models;
namespace WebApi.DTOs.In;

public class ManagerCreateModel
{
    public string Description { get; set; }
    public int DepartmentId { get; set; }
    public int CategoryId { get; set; }
}
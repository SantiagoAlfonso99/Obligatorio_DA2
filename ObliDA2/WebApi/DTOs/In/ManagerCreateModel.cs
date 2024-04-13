using Domain.Models;
namespace WebApi.DTOs.In;

public class ManagerCreateModel
{
    public string Description { get; set; }
    public string Department { get; set; }
    public string Category { get; set; }
}
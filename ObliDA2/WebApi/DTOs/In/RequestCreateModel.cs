using Domain.Models;
namespace WebApi.DTOs.In;

public class RequestCreateModel
{
    public string Description { get; set; }
    public string Department { get; set; }
    public string Category { get; set; }
}
using Domain.Models;

namespace WebApi.DTOs.Out;
public class RequestDetailModel
{
    public int Id { get; set; }
    public string Description { get; set; }
    public string Department { get; set; }
    public string Category { get; set; }
    public string Status { get; set; }

    public RequestDetailModel(Request request)
    {
        Id = request.Id;
        Description = request.Description;
        Department = request.Department;
        Category = request.Category;
        Status = request.Status.ToString();
    }
}
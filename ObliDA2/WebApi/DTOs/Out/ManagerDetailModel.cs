using Domain.Models;

namespace WebApi.DTOs.Out;
public class ManagerDetailModel
{
    public int Id { get; set; }
    public string Description { get; set; }
    public Apartment Department { get; set; }
    public string Category { get; set; }
    public string Status { get; set; }

    public ManagerDetailModel(Request request)
    {
        Id = request.Id;
        Description = request.Description;
        Department = request.Department;
        Category = request.Category.Name;
        Status = request.Status.ToString();
    }
    
    public override bool Equals(object obj)
    {
        ManagerDetailModel other = (ManagerDetailModel)obj;
        return Id == other.Id
               && Category == other.Category
               && Description == other.Description
               && Status == other.Status;
    }
}
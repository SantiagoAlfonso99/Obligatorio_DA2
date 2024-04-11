using Domain.Models;
namespace WebApi.DTOs.Out;

public class CategoryDetailModel
{
    public CategoryDetailModel(Category categoryIn)
    {
        Id = categoryIn.Id;
        Name = categoryIn.Name;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    
    public override bool Equals(object? obj)
    {
        var otherCategoryDetail = obj as CategoryDetailModel;
        return Id == otherCategoryDetail.Id
               && Name == otherCategoryDetail.Name;
    }
}
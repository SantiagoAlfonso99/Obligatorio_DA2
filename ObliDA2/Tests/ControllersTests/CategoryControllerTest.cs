using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;

namespace Tests.ControllersTests;

[TestClass]
public class CategoryControllerTest
{
    [TestMethod]
    public void IndexOk()
    {
        Mock<ICategoryLogic> service = new Mock<ICategoryLogic>();
        List<Category> categories = new List<Category>() { new Category(){ Id = 1 } };
        service.Setup(logic => logic.GetAll()).Returns(categories);
        CategoryController controller = new CategoryController(service.Object);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<Category> returnedCategories = okResult.Value as List<Category>;
        List<Category> expectedList = new List<Category>() { new Category() { Id = 1 } };
        CollectionAssert.AreEqual(expectedList, returnedCategories);
    }
}
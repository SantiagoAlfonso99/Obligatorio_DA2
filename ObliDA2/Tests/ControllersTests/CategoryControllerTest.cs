using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;

namespace Tests.ControllersTests;

[TestClass]
public class CategoryControllerTest
{
    private Mock<ICategoryLogic> service;
    private Category expectedCategory;
    
    [TestInitialize]
    public void Initialize()
    {
        service = new Mock<ICategoryLogic>();
        
    }
    
    [TestMethod]
    public void IndexOk()
    {
        List<Category> categories = new List<Category>() { new Category(){ Id = 1 } };
        service.Setup(logic => logic.GetAll()).Returns(categories);
        CategoryController controller = new CategoryController(service.Object);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<Category> returnedCategories = okResult.Value as List<Category>;
        List<Category> expectedList = new List<Category>() { new Category() { Id = 1 } };
        
        service.VerifyAll();
        CollectionAssert.AreEqual(expectedList, returnedCategories);
    }
    
    [TestMethod]
    public void ShowOk()
    {
        Mock<ICategoryLogic> service = new Mock<ICategoryLogic>();
        Category consultedCategory = new Category() { Id = 1 };
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(consultedCategory);
        CategoryController controller = new CategoryController(service.Object);
        
        var result = controller.Show(1);
        var okResult = result as OkObjectResult;
        Category returnedCategory = okResult.Value as Category;
        Category expectedCategory = new Category() { Id = 1 };
        
        service.VerifyAll();
        Assert.AreEqual(expectedCategory, returnedCategory);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        Mock<ICategoryLogic> service = new Mock<ICategoryLogic>();
        Category consultedCategory = new Category() { Id = 1 };
        service.Setup(logic => logic.Create(It.IsAny<string>())).Returns(consultedCategory);
        CategoryController controller = new CategoryController(service.Object);
        
        var result = controller.Create("name");
        var okResult = result as OkObjectResult;
        Category returnedCategory = okResult.Value as Category;
        Category expectedCategory = new Category() { Id = 1 };
        
        service.VerifyAll();
        Assert.AreEqual(expectedCategory, returnedCategory);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Mock<ICategoryLogic> service = new Mock<ICategoryLogic>();
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(true);
        CategoryController controller = new CategoryController(service.Object);
        
        var result = controller.Delete(1);
        var noContentResult = result as NoContentResult;
        
        service.VerifyAll();
        
        Assert.IsNotNull(noContentResult);
    }
    
    [TestMethod]
    public void DeleteNotFoundElement()
    {
        Mock<ICategoryLogic> service = new Mock<ICategoryLogic>();
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(false);
        CategoryController controller = new CategoryController(service.Object);
        
        var result = controller.Delete(1);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty("Message");
        
        service.VerifyAll();
        
        Assert.AreEqual("The deletion action could not be completed because there is no Category with that ID", message.GetValue(notFoundResult.Value));
    }
}
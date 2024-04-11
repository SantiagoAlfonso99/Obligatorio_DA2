using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;

namespace Tests.ControllersTests;

[TestClass]
public class CategoryControllerTest
{
    private Mock<ICategoryLogic> service;
    private Category expectedCategory;
    private const int UserId = 1;
    private const string PropertyName = "Message";
    private const string CreateCategoryExceptionMess = "Please make sure not to input null or empty data.";
    private const string CreateCategoryLogicExceptionMess =
        "Unable to create category because a category with that name already exists.";
    private const string DeleteNotFoundMes =
        "The deletion action could not be completed because there is no Category with that ID";
    private const string ShowNotFoundMes =
        "The show action could not be completed because there is no Category with that ID";
    
    [TestInitialize]
    public void Initialize()
    {
        service = new Mock<ICategoryLogic>();
        
    }
    
    [TestMethod]
    public void IndexOk()
    {
        List<Category> categories = new List<Category>() { new Category(){ Id = UserId } };
        service.Setup(logic => logic.GetAll()).Returns(categories);
        CategoryController controller = new CategoryController(service.Object);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<CategoryDetailModel> returnedCategories = okResult.Value as List<CategoryDetailModel>;
        List<Category> expectedList = new List<Category>() { new Category() { Id = UserId } };
        List<CategoryDetailModel> expectedDetailModels =
            expectedList.Select(category => new CategoryDetailModel(category)).ToList();
        
        service.VerifyAll();
        CollectionAssert.AreEqual(expectedDetailModels, returnedCategories);
    }
    
    [TestMethod]
    public void ShowOk()
    {
        Mock<ICategoryLogic> service = new Mock<ICategoryLogic>();
        Category consultedCategory = new Category() { Id = UserId };
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(consultedCategory);
        CategoryController controller = new CategoryController(service.Object);
        
        var result = controller.Show(UserId);
        var okResult = result as OkObjectResult;
        CategoryDetailModel returnedCategory = okResult.Value as CategoryDetailModel;
        CategoryDetailModel expectedCategory = new CategoryDetailModel(consultedCategory);
        
        service.VerifyAll();
        Assert.AreEqual(expectedCategory, returnedCategory);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        Mock<ICategoryLogic> service = new Mock<ICategoryLogic>();
        Category consultedCategory = new Category() { Id = UserId };
        service.Setup(logic => logic.Create(It.IsAny<Category>())).Returns(consultedCategory);
        CategoryController controller = new CategoryController(service.Object);
        CategoryCreateModel newCategory = new CategoryCreateModel() { Name = "pepe" };
            
        var result = controller.Create(newCategory);
        var okResult = result as OkObjectResult;
        CategoryDetailModel returnedCategory = okResult.Value as CategoryDetailModel;
        CategoryDetailModel expectedCategory = new CategoryDetailModel(consultedCategory);
        
        service.VerifyAll();
        Assert.AreEqual(expectedCategory, returnedCategory);
    }
    
    [TestMethod]
    public void CreateCatchInvalidLogicException()
    {
        Mock<ICategoryLogic> service = new Mock<ICategoryLogic>();
        Category consultedCategory = new Category() { Id = UserId };
        service.Setup(logic => logic.Create(It.IsAny<Category>())).Throws(new InvalidCategoryLogicException());
        CategoryController controller = new CategoryController(service.Object);
        CategoryCreateModel newCategory = new CategoryCreateModel() { Name = "CategoryName" };
            
        var result = controller.Create(newCategory);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty(PropertyName);
        
        service.VerifyAll();
        Assert.AreEqual(CreateCategoryLogicExceptionMess, message.GetValue(notFoundResult.Value));
    }
    
    [TestMethod]
    public void CreateCatchInvalidCategoryException()
    {
        Mock<ICategoryLogic> service = new Mock<ICategoryLogic>();
        Category consultedCategory = new Category() { Id = UserId };
        service.Setup(logic => logic.Create(It.IsAny<Category>())).Throws(new InvalidCategoryException());
        CategoryController controller = new CategoryController(service.Object);
        CategoryCreateModel newCategory = new CategoryCreateModel() { Name = "CategoryName" };
            
        var result = controller.Create(newCategory);
        var badRequestResult = result as BadRequestObjectResult;
        var message = badRequestResult.Value.GetType().GetProperty(PropertyName);
        
        service.VerifyAll();
        Assert.AreEqual(CreateCategoryExceptionMess, message.GetValue(badRequestResult.Value));
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Mock<ICategoryLogic> service = new Mock<ICategoryLogic>();
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(true);
        CategoryController controller = new CategoryController(service.Object);
        
        var result = controller.Delete(UserId);
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
        
        var result = controller.Delete(UserId);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty(PropertyName);
        
        service.VerifyAll();
        
        Assert.AreEqual(DeleteNotFoundMes, message.GetValue(notFoundResult.Value));
    }
    
    [TestMethod]
    public void ShowNotFoundElement()
    {
        Mock<ICategoryLogic> service = new Mock<ICategoryLogic>();
        Category consultedCategory = new Category() { Id = UserId };
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Throws(new InvalidCategoryLogicException());
        CategoryController controller = new CategoryController(service.Object);
        
        var result = controller.Show(UserId);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty(PropertyName);
        
        service.VerifyAll();
        
        Assert.AreEqual(ShowNotFoundMes, message.GetValue(notFoundResult.Value));
    }
}
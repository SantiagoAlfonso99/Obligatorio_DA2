using BusinessLogic.IRepository;
using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;

namespace Tests.ServicesTests;

[TestClass]
public class CategoryLogicTests
{
    private Mock<ICategoryRepository> repo;
    private Category consultedCategory;
    private const int UserId = 1;
    private const bool TrueSuccess = true;
    private const bool FalseSuccess = false;
    
    [TestInitialize]
    public void Initialize()
    {
        repo = new Mock<ICategoryRepository>();
        consultedCategory = new Category() { Id = UserId };
    }
    
    [TestMethod]
    public void GetByIdOk()
    {
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(consultedCategory);
        CategoryLogic service = new CategoryLogic(repo.Object);

        Category returnedCategory = service.GetById(1);
        Category expectedCategory = new Category() { Id = UserId };
        
        repo.VerifyAll();
        Assert.AreEqual(expectedCategory, returnedCategory);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidCategoryLogicException))]
    public void GetByIdThrowsException()
    {
        Category nullCategory = null;
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(nullCategory);
        CategoryLogic service = new CategoryLogic(repo.Object);

        Category returnedCategory = service.GetById(1);
        Category expectedCategory = new Category() { Id = UserId };
        
        repo.VerifyAll();
        Assert.AreEqual(expectedCategory, returnedCategory);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidCategoryException))]
    public void CreateCategoryWithEmptyNameThrowsException()
    {
        Category newCategory = new Category() { Id = UserId, Name = "" };
        repo.Setup(repository => repository.GetAll()).Returns(new List<Category>());
        repo.Setup(repository => repository.Create(It.IsAny<Category>()));
        CategoryLogic service = new CategoryLogic(repo.Object);

        Category returnedCategory = service.Create(newCategory);
        
        repo.VerifyAll();
        Assert.AreEqual(returnedCategory, newCategory);
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        List<Category> consultedCategories = new List<Category>() { consultedCategory };
        repo.Setup(repository => repository.GetAll()).Returns(consultedCategories);
        CategoryLogic service = new CategoryLogic(repo.Object);

        List<Category> returnedCategory = service.GetAll();
        List<Category> expectedList = new List<Category>() { consultedCategory};
        
        repo.VerifyAll();
        CollectionAssert.AreEqual(expectedList, returnedCategory);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        Category newCategory = new Category() { Id = UserId, Name = "CategoryName" };
        repo.Setup(repository => repository.GetAll()).Returns(new List<Category>());
        repo.Setup(repository => repository.Create(It.IsAny<Category>()));
        CategoryLogic service = new CategoryLogic(repo.Object);

        Category returnedCategory = service.Create(newCategory);
        
        repo.VerifyAll();
        Assert.AreEqual(returnedCategory, newCategory);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidCategoryLogicException))]
    public void CreateCategoryWithRepeatedNameThrowsException()
    {
        Category newCategory = new Category() { Id = UserId, Name = "CategoryName" };
        List<Category> consultedList = new List<Category>() {new Category() { Id = 5, Name = "CategoryName" } };
        repo.Setup(repository => repository.GetAll()).Returns(consultedList);
        repo.Setup(repository => repository.Create(It.IsAny<Category>()));
        CategoryLogic service = new CategoryLogic(repo.Object);

        Category returnedCategory = service.Create(newCategory);
        
        repo.VerifyAll();
        Assert.AreEqual(returnedCategory, newCategory);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(consultedCategory);
        repo.Setup(repository => repository.Delete(It.IsAny<Category>()));
        CategoryLogic service = new CategoryLogic(repo.Object);

        bool success = service.Delete(UserId);
        
        repo.VerifyAll();
        Assert.AreEqual(TrueSuccess, success);
    }
    
    [TestMethod]
    public void DeleteReturnsFalseForNonExistentCategory()
    {
        Category nullCategory = null;
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(nullCategory);
        CategoryLogic service = new CategoryLogic(repo.Object);

        bool success = service.Delete(UserId);
        
        repo.VerifyAll();
        Assert.AreEqual(FalseSuccess, success);
    }
}
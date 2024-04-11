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
    [TestMethod]
    public void GetByIdOk()
    {
        Mock<ICategoryRepository> repo = new Mock<ICategoryRepository>();
        Category consultedCategory = new Category() { Id = 1 };
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(consultedCategory);
        CategoryLogic service = new CategoryLogic(repo.Object);

        Category returnedCategory = service.GetById(1);
        Category expectedCategory = new Category() { Id = 1 };
        
        repo.VerifyAll();
        Assert.AreEqual(expectedCategory, returnedCategory);
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        Mock<ICategoryRepository> repo = new Mock<ICategoryRepository>();
        List<Category> consultedCategories = new List<Category>() { new Category(){Id = 1} };
        repo.Setup(repository => repository.GetAll()).Returns(consultedCategories);
        CategoryLogic service = new CategoryLogic(repo.Object);

        List<Category> returnedCategory = service.GetAll();
        List<Category> expectedList = new List<Category>() { new Category(){Id = 1}};
        
        repo.VerifyAll();
        CollectionAssert.AreEqual(expectedList, returnedCategory);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        Mock<ICategoryRepository> repo = new Mock<ICategoryRepository>();
        Category newCategory = new Category() { Id = 1 };
        repo.Setup(repository => repository.Create(It.IsAny<Category>()));
        CategoryLogic service = new CategoryLogic(repo.Object);

        Category returnedCategory = service.Create(newCategory);
        
        repo.VerifyAll();
        Assert.AreEqual(returnedCategory, newCategory);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Mock<ICategoryRepository> repo = new Mock<ICategoryRepository>();
        Category consultedCategory = new Category() { Id = 1 };
        repo.Setup(repository => repository.GetById(It.IsAny<int>())).Returns(consultedCategory);
        repo.Setup(repository => repository.Delete(It.IsAny<Category>()));
        CategoryLogic service = new CategoryLogic(repo.Object);

        bool success = service.Delete(1);
        
        repo.VerifyAll();
        Assert.AreEqual(true, success);
    }
}
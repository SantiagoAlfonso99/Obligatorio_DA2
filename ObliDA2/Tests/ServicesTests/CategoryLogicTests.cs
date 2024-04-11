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
}
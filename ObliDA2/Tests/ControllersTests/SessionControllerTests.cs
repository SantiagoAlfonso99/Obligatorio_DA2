using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;
using WebApi.DTOs.In;

namespace Tests.ControllersTests;

[TestClass]
public class SessionControllerTests
{
    [TestMethod]
    public void LogInOk()
    {
        Admin admin = new Admin()
            { Email = "pepe123@gmail.com", Name = "Pepe", LastName = "Rodriguez", Password = "perez123", Id = 1 };
        Mock<IUsersLogic> userService = new Mock<IUsersLogic>();
        userService.Setup(service => service.LogIn(It.IsAny<string>(), It.IsAny<string>())).Returns(admin);
        SessionController controller = new SessionController(userService.Object);
        LogInDTO inputs = new LogInDTO() { Email = "pepe123@gmail.com", Password = "perez123" }; 
        var result = controller.LogIn(inputs);
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
    }
}
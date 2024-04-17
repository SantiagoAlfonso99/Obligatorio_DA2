using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;

namespace Tests.ControllersTests;

[TestClass]
public class ApartmentOwnerControllerTests
{
    [TestMethod]
    public void CreateOk()
    {
        Mock<IApartmentOwnerLogic> service = new Mock<IApartmentOwnerLogic>();
        ApartmentOwner owner = new ApartmentOwner() { Id = 1 };
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(owner);
        ApartmentOwnerController controller = new ApartmentOwnerController(service.Object);
        
        var result = controller.Show(1);
        var okResult = result as OkObjectResult;
        ApartmentOwner returnedOwner = okResult.Value as ApartmentOwner;
        ApartmentOwner expected = new ApartmentOwner() { Id = 1 };
        
        service.VerifyAll();
        Assert.AreEqual(returnedOwner, expected);
    }
}
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
    private const int UserId = 1;
    
    [TestMethod]
    public void GetByIdOk()
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
    
    [TestMethod]
    public void CreateOk()
    {
        Mock<IApartmentOwnerLogic> service = new Mock<IApartmentOwnerLogic>();
        ApartmentOwner owner = new ApartmentOwner() { Id = 1 };
        service.Setup(logic => logic.Create(It.IsAny<ApartmentOwner>())).Returns(owner);
        ApartmentOwnerController controller = new ApartmentOwnerController(service.Object);
        
        var result = controller.Create(new ApartmentOwner(){Id = 1});
        var okResult = result as OkObjectResult;
        ApartmentOwner returnedOwner = okResult.Value as ApartmentOwner;
        ApartmentOwner expected = new ApartmentOwner() { Id = 1 };
        
        service.VerifyAll();
        Assert.AreEqual(returnedOwner, expected);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        Mock<IApartmentOwnerLogic> service = new Mock<IApartmentOwnerLogic>();
        ApartmentOwner owner = new ApartmentOwner() { Id = 1 };
        service.Setup(logic => logic.Update(It.IsAny<int>(), It.IsAny<ApartmentOwner>())).Returns(owner);
        ApartmentOwnerController controller = new ApartmentOwnerController(service.Object);
        
        var result = controller.Update(1, new ApartmentOwner(){Id = 1});
        var okResult = result as OkObjectResult;
        ApartmentOwner returnedOwner = okResult.Value as ApartmentOwner;
        ApartmentOwner expected = new ApartmentOwner() { Id = 1 };
        
        service.VerifyAll();
        Assert.AreEqual(returnedOwner, expected);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Mock<IApartmentOwnerLogic> service = new Mock<IApartmentOwnerLogic>();
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(true);
        ApartmentOwnerController controller = new ApartmentOwnerController(service.Object);
        
        var result = controller.Delete(1);
        var okResult = result as OkObjectResult;
        var noContentResult = result as NoContentResult;

        service.VerifyAll();
        Assert.IsNotNull(noContentResult);
    }
}
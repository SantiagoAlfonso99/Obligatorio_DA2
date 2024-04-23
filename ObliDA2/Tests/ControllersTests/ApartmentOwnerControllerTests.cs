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
    private Mock<IApartmentOwnerLogic> service;
    private ApartmentOwner newApartmentOwner;
    private ApartmentOwnerDetailModel expectedModel;
    private const string PropertyName = "Message";
    private const string NotFoundMessage =
        "The deletion action could not be completed because there is no apartment owner with that ID";
    
    [TestInitialize]
    public void Initialize()
    {
        service = new Mock<IApartmentOwnerLogic>();
        newApartmentOwner = new ApartmentOwner() { Id = 1, Name = "Pepito", LastName = "sanchez", Email = "pepe2@gmail.com"};
        ApartmentOwner otherOwner = new ApartmentOwner() { Id = 1, Name = "Pepito", LastName = "sanchez", Email = "pepe2@gmail.com"};
        expectedModel = new ApartmentOwnerDetailModel(otherOwner);
    }
    
    [TestMethod]
    public void GetByIdOk()
    {
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(newApartmentOwner);
        ApartmentOwnerController controller = new ApartmentOwnerController(service.Object);
        
        var result = controller.Show(UserId);
        var okResult = result as OkObjectResult;
        ApartmentOwnerDetailModel returnedOwner = okResult.Value as ApartmentOwnerDetailModel;
        
        service.VerifyAll();
        Assert.AreEqual(expectedModel, returnedOwner);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        service.Setup(logic => logic.Create(It.IsAny<ApartmentOwner>())).Returns(newApartmentOwner);
        ApartmentOwnerController controller = new ApartmentOwnerController(service.Object);
        
        var result = controller.Create(new ApartmentOwnerCreateModel(){Name = "Pepito", LastName = "sanchez", Email = "pepe2@gmail.com"});
        var okResult = result as OkObjectResult;
        ApartmentOwnerDetailModel returnedOwner = okResult.Value as ApartmentOwnerDetailModel;
        
        service.VerifyAll();
        Assert.AreEqual(returnedOwner, expectedModel);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        ApartmentOwner owner = new ApartmentOwner() { Id = 1, Name = "Pepito", LastName = "sanchez", Email = "pepe2@gmail.com"};
        service.Setup(logic => logic.Update(It.IsAny<int>(), It.IsAny<ApartmentOwner>())).Returns(owner);
        ApartmentOwnerController controller = new ApartmentOwnerController(service.Object);
        
        var result = controller.Update(UserId, new ApartmentOwnerCreateModel(){Name = "Pepito", LastName = "sanchez", Email = "pepe2@gmail.com"});
        var okResult = result as OkObjectResult;
        ApartmentOwnerDetailModel returnedOwner = okResult.Value as ApartmentOwnerDetailModel;
        
        service.VerifyAll();
        Assert.AreEqual(returnedOwner, expectedModel);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(true);
        ApartmentOwnerController controller = new ApartmentOwnerController(service.Object);
        
        var result = controller.Delete(UserId);
        var okResult = result as OkObjectResult;
        var noContentResult = result as NoContentResult;

        service.VerifyAll();
        Assert.IsNotNull(noContentResult);
    }
    
    [TestMethod]
    public void DeleteReturnNotFoundOk()
    {
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(false);
        ApartmentOwnerController controller = new ApartmentOwnerController(service.Object);
        
        var result = controller.Delete(UserId);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty(PropertyName);
        
        service.VerifyAll();
        Assert.AreEqual(NotFoundMessage, message.GetValue(notFoundResult.Value));
    }
}
using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;

namespace Tests.ControllersTests;
[TestClass]
public class ApartmentsControllerTests
{
    [TestMethod]
    public void GetAllOk()
    {
        Mock<IApartmentLogic> service = new Mock<IApartmentLogic>();
        List<Apartment> apartments = new List<Apartment>(){new Apartment(){Id = 1}};
        service.Setup(logic => logic.GetAll()).Returns(apartments);
        ApartmentController controller = new ApartmentController(service.Object);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<Apartment> returnedApartments = okResult.Value as List<Apartment>;
        List<Apartment> expectedList = new List<Apartment>(){new Apartment(){Id = 1}};
        
        CollectionAssert.AreEqual(returnedApartments, expectedList);
    }
    
    [TestMethod]
    public void ShowOk()
    {
        Mock<IApartmentLogic> service = new Mock<IApartmentLogic>();
        Apartment consultedApartment = new Apartment() { Id = 1 };
        service.Setup(logic => logic.GetById(It.IsAny<int>())).Returns(consultedApartment);
        ApartmentController controller = new ApartmentController(service.Object);
        
        var result = controller.Show(1);
        var okResult = result as OkObjectResult;
        Apartment returnedApartment = okResult.Value as Apartment;
        Apartment expectedModel = new Apartment{Id = 1};
        
        Assert.AreEqual(returnedApartment, expectedModel);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        Mock<IApartmentLogic> service = new Mock<IApartmentLogic>();
        Apartment consultedApartment = new Apartment() { Id = 1 };
        service.Setup(logic => logic.Create(It.IsAny<Apartment>())).Returns(consultedApartment);
        ApartmentController controller = new ApartmentController(service.Object);
        
        var result = controller.Create(new Apartment(){Id = 1});
        var okResult = result as OkObjectResult;
        Apartment returnedApartment = okResult.Value as Apartment;
        Apartment expectedModel = new Apartment{Id = 1};
        
        Assert.AreEqual(returnedApartment, expectedModel);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Mock<IApartmentLogic> service = new Mock<IApartmentLogic>();
        service.Setup(logic => logic.Delete(It.IsAny<int>())).Returns(true);
        ApartmentController controller = new ApartmentController(service.Object);
        
        var result = controller.Delete(1);
        var noContentResult = result as NoContentResult;
        
        service.VerifyAll();
        Assert.IsNotNull(noContentResult);
    }
}
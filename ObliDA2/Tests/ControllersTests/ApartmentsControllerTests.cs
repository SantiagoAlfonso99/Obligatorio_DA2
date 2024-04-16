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
    public void CreateOk()
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
}
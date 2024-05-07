using BusinessLogic.IRepository;
using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;
using WebApi.DTOs.In;
using WebApi.DTOs.Out;

namespace Tests.ControllersTests;

[TestClass]
public class InvitationControllerTest
{
    private Invitation expectedInvitation;
    private Mock<IInvitationLogic> invitationLogicMock;
    private Mock<IManagerLogic> managerLogic;
    private Mock<ICompanyAdminLogic> companyAdminLogic;
    private Mock<IUsersLogic> usersLogic;
    private const int UserId = 1;
    private const string EmailResponse = "pepe@gmail.com";
    private const string Password = "pepe";
    private const string AcceptInvitationBadRequest = "Please ensure to enter a non-null or non-empty email and password";
    private const string PropertyName = "Message";
    private const string Name = "pepe";
    private const string ExpectedMessage = "The deletion action could not be completed because there is no invitation with that ID";
    private const bool ReturnsFalse = false;
    private const bool ReturnsTrue = true;
    private InvitationResponse response;
    private InvitationController controller;
    
    [TestInitialize]
    public void Initialize()
    {
        managerLogic = new Mock<IManagerLogic>();
        usersLogic = new Mock<IUsersLogic>();
        companyAdminLogic = new Mock<ICompanyAdminLogic>();
        expectedInvitation = new Invitation(){Id = UserId, Name = Name, DeadLine = DateTime.Now.AddDays(4), RecipientEmail = EmailResponse, Status = "Pending", Role = "Manager"};
        invitationLogicMock = new Mock<IInvitationLogic>();
        response = new InvitationResponse()
            { Email = EmailResponse, acceptInvitation = true, Password = Password };
        controller = new InvitationController(invitationLogicMock.Object, managerLogic.Object, companyAdminLogic.Object, usersLogic.Object);
    }
    
    [TestMethod]
    public void IndexOkTest()
    {
        List <Invitation> invitations = new List<Invitation>() { expectedInvitation};
        invitationLogicMock.Setup(r => r.GetAll()).Returns(invitations);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<InvitationDetailModel> returnedInvitations = okResult.Value as List<InvitationDetailModel>;
        List<InvitationDetailModel> expectedList =
            invitations.Select(invitation => new InvitationDetailModel(invitation)).ToList();
            
        invitationLogicMock.VerifyAll();
        CollectionAssert.AreEqual(
            expectedList,
            returnedInvitations
        );
    }
    
    [TestMethod]
    public void ShowOkTest()
    {
        invitationLogicMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(expectedInvitation);
        
        var result = controller.Show(UserId);
        var okResult = result as OkObjectResult;
        InvitationDetailModel returnedInvitation = okResult.Value as InvitationDetailModel;
        
        invitationLogicMock.VerifyAll();
        Assert.AreEqual(
            new InvitationDetailModel(expectedInvitation),
             returnedInvitation
        );
    }
    
    [TestMethod]
    public void CreateOkTest()
    {
        expectedInvitation = new Invitation(){Id = UserId, Name = Name, DeadLine = DateTime.Now.AddDays(4), RecipientEmail = EmailResponse, Status = "Pending", Role = "Manager"};
        InvitationCreateModel newInvitation = new InvitationCreateModel() { DeadLine = DateTime.Now.AddDays(3), Email = "pep@gmail.com", Name = "pep", Role = "Manager"};
        invitationLogicMock.Setup(r => r.Create(It.IsAny<Invitation>())).Returns(expectedInvitation);
        usersLogic.Setup(logic => logic.ValidateEmail(It.IsAny<string>()));
        
        var result = controller.Create(newInvitation);
        var okResult = result as OkObjectResult;
        InvitationDetailModel returnedInvitation = okResult.Value as InvitationDetailModel;
    
        invitationLogicMock.VerifyAll();
        Assert.AreEqual(
            new InvitationDetailModel(expectedInvitation),
            returnedInvitation
        );
    }
    
    [TestMethod]
    public void CreateInvitationWithInvalidRoleNameThrowsException()
    {
        expectedInvitation = new Invitation(){Id = UserId, Name = Name, DeadLine = DateTime.Now.AddDays(4), RecipientEmail = EmailResponse, Status = "Pending", Role = "Manager"};
        InvitationCreateModel newInvitation = new InvitationCreateModel() { DeadLine = DateTime.Now.AddDays(3), Email = "pep@gmail.com", Name = "pep", Role = "Manage"};
        invitationLogicMock.Setup(r => r.Create(It.IsAny<Invitation>())).Returns(expectedInvitation);
        
        var result = controller.Create(newInvitation);
        var badRequestResult = result as BadRequestObjectResult;
        var message = badRequestResult.Value.GetType().GetProperty(PropertyName);

        Assert.AreEqual("Invalid role name", message.GetValue(badRequestResult.Value));
    }
    
    [TestMethod]
    public void DeleteOkTest()
    {
        invitationLogicMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(ReturnsTrue);
        
        var result = controller.Delete(UserId);
        var noContentResult = result as NoContentResult;
        
        invitationLogicMock.VerifyAll();
        
        Assert.IsNotNull(noContentResult);
    }
    
    [TestMethod]
    public void DeleteNotFoundTest()
    {
        invitationLogicMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(ReturnsFalse);
        
        var result = controller.Delete(UserId);
        var notFoundResult = result as NotFoundObjectResult;
        var message = notFoundResult.Value.GetType().GetProperty(PropertyName);
        
        invitationLogicMock.VerifyAll();

        Assert.AreEqual(ExpectedMessage, message.GetValue(notFoundResult.Value));
    }
    
    [TestMethod]
    public void ManagerAcceptInvitationOkTest()
    {
        invitationLogicMock.Setup(r => r.InvitationResponse(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(expectedInvitation);
        managerLogic.Setup(service => service.Create(It.IsAny<Manager>())).Returns(new Manager(){Email = "pepe@gmail.com",Password = "password"});
        
        var result = controller.InvitationResponse(UserId, response);
        var okResult = result as OkObjectResult;
        InvitationDetailModel returnedInvitation = okResult.Value as InvitationDetailModel;
        
        invitationLogicMock.VerifyAll();
        managerLogic.VerifyAll();
            
        Assert.AreEqual(new InvitationDetailModel(expectedInvitation), returnedInvitation);
    }
    
    [TestMethod]
    public void CompanyAdminAcceptInvitationOkTest()
    {
        expectedInvitation.Role = "CompanyAdmin";
        invitationLogicMock.Setup(r => r.InvitationResponse(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(expectedInvitation);
        companyAdminLogic.Setup(service => service.Create(It.IsAny<CompanyAdmin>())).Returns(new CompanyAdmin(){Email = "pepe@gmail.com",Password = "password"});
        
        var result = controller.InvitationResponse(UserId, response);
        var okResult = result as OkObjectResult;
        InvitationDetailModel returnedInvitation = okResult.Value as InvitationDetailModel;
        
        invitationLogicMock.VerifyAll();
        managerLogic.VerifyAll();
            
        Assert.AreEqual(new InvitationDetailModel(expectedInvitation), returnedInvitation);
    }
    
    [TestMethod]
    public void UserAcceptInvitationBadRequestTest()
    {
        InvitationResponse response = new InvitationResponse()
            { Email = "", acceptInvitation = true, Password = "" };
        invitationLogicMock.Setup(r => r.InvitationResponse(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(expectedInvitation);
        
        var result = controller.InvitationResponse(UserId, response);
        var badRequestResult = result as BadRequestObjectResult;
        var message = badRequestResult.Value.GetType().GetProperty(PropertyName);

        Assert.AreEqual(AcceptInvitationBadRequest, message.GetValue(badRequestResult.Value));
    }
    
}
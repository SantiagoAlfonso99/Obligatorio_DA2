using Domain.Exceptions;
using IBusinessLogic;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers;
using Moq;

namespace Tests.ControllersTests;

[TestClass]
public class InvitationControllerTest
{
    public void IndexOkTest()
    {
        List <Invitation> invitations = new List<Invitation>() { new Invitation(){Id = 1}};
        Mock<IInvitationLogic> invitationLogicMock = new Mock<IInvitationLogic>();
        invitationLogicMock.Setup(r => r.GetAll()).Returns(invitations);
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.Index();
        var okResult = result as OkObjectResult;
        List<Invitation> returnedInvitations = okResult.Value as List<Invitation>;
        
        invitationLogicMock.VerifyAll();
        CollectionAssert.AreEqual(
            invitations,
            returnedInvitations
        );
    }
    
    public void ShowOkTest()
    {
        Invitation expectedInvitation = new Invitation() { Id = 1 };
        List <Invitation> invitations = new List<Invitation>() { expectedInvitation};
        Mock<IInvitationLogic> invitationLogicMock = new Mock<IInvitationLogic>();
        invitationLogicMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(expectedInvitation);
        InvitationController controller = new InvitationController(invitationLogicMock.Object);
        
        var result = controller.Show(1);
        var okResult = result as OkObjectResult;
        Invitation returnedInvitation = okResult.Value as Invitation;
        
        invitationLogicMock.VerifyAll();
        Assert.AreEqual(
            expectedInvitation,
             returnedInvitation
        );
    }
}
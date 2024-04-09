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
    
}
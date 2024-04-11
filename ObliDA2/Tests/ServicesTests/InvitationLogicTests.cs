using System.Data.Common;
using Domain.Exceptions;
using Domain.Models;
using BusinessLogic.Services;
using BusinessLogic.IRepository;
using Moq;
namespace Tests.ServicesTests;

[TestClass]
public class InvitationLogicTests
{
    private Mock<IInvitationRepository> repo;
    private const int UserId = 1;
    private const int CreatorId = 2;
    private const string ValidName = "pepe";
    private const string ValidEmail = "pepe@gmail.com";
    private const string ValidStatus = "Pending";
    private const string AcceptedStatus = "Accepted";
    private const string RejectedStatus = "Rejected";
    private const bool TrueSuccess = true;
    private const bool FalseSuccess = false;
    private Invitation expectedInvitation;
    
    [TestInitialize]
    public void Initialize()
    {
        repo = new Mock<IInvitationRepository>();
        expectedInvitation = new Invitation() {CreatorId = CreatorId, DeadLine = DateTime.Now.AddDays(4), Name = ValidName, RecipientEmail = ValidEmail, Status = ValidStatus};
    }
    
    [TestMethod]
    public void GetAllReturnElements()
    {
        List<Invitation> invitations = new List<Invitation>() { new Invitation(){Id = UserId}};
        repo.Setup(invitationRepo => invitationRepo.GetAll()).Returns(invitations);
        InvitationLogic service = new InvitationLogic(repo.Object);

        List<Invitation> returnedList = service.GetAll();
        repo.VerifyAll();
        CollectionAssert.AreEqual(returnedList, invitations);
    }
    
    [TestMethod]
    public void GetByIdReturnElement()
    {
        Invitation invitation = new Invitation() { Id = UserId };
        repo.Setup(invitationRepo => invitationRepo.GetById(It.IsAny<int>())).Returns(invitation);
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        Invitation returnedInvitation = service.GetById(1);
        repo.VerifyAll();
        Assert.AreEqual(returnedInvitation, invitation);
    }
    
    [TestMethod]
    public void CreateInvitationOk()
    {
        repo.Setup(invitationRepo => invitationRepo.GetAll()).Returns(new List<Invitation>());
        repo.Setup(invitationRepo => invitationRepo.Create(It.IsAny<Invitation>()));
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        Invitation returnedInvitation = service.Create(expectedInvitation);
        repo.VerifyAll();
        Assert.AreEqual(returnedInvitation, expectedInvitation);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidInvitationException))]
    public void CreateInvitationWithEmptyEmailThrowsException()
    {
        Invitation invitation = new Invitation() {CreatorId = CreatorId, DeadLine = DateTime.Now.AddDays(4), Name = ValidName, RecipientEmail = ""};
        repo.Setup(invitationRepo => invitationRepo.GetAll()).Returns(new List<Invitation>());
        repo.Setup(invitationRepo => invitationRepo.Create(It.IsAny<Invitation>()));
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        Invitation returnedInvitation = service.Create(invitation);
        repo.VerifyAll();
        Assert.AreEqual(returnedInvitation, expectedInvitation);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidInvitationException))]
    public void CreateInvitationWithEmptyNameThrowsException()
    {
        Invitation invitation = new Invitation() {CreatorId = CreatorId, DeadLine = DateTime.Now.AddDays(4), Name = "", RecipientEmail = ValidEmail};
        repo.Setup(invitationRepo => invitationRepo.GetAll()).Returns(new List<Invitation>());
        repo.Setup(invitationRepo => invitationRepo.Create(It.IsAny<Invitation>()));
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        Invitation returnedInvitation = service.Create(invitation);
        repo.VerifyAll();
        Assert.AreEqual(returnedInvitation, expectedInvitation);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidInvitationException))]
    public void CreateInvitationWithEmptyStatusThrowsException()
    {
        Invitation invitation = new Invitation() {CreatorId = CreatorId, DeadLine = DateTime.Now.AddDays(4), Name = ValidName, RecipientEmail = ValidEmail, Status = ""};
        repo.Setup(invitationRepo => invitationRepo.GetAll()).Returns(new List<Invitation>());
        repo.Setup(invitationRepo => invitationRepo.Create(It.IsAny<Invitation>()));
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        Invitation returnedInvitation = service.Create(invitation);
        repo.VerifyAll();
        Assert.AreEqual(returnedInvitation, expectedInvitation);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidInvitationException))]
    public void CreateInvitationWithInvalidStatusThrowsException()
    {
        Invitation invitation = new Invitation() {CreatorId = 2, DeadLine = DateTime.Now.AddDays(4), Name = ValidName, RecipientEmail = ValidEmail, Status = "invalidStatus"};
        repo.Setup(invitationRepo => invitationRepo.GetAll()).Returns(new List<Invitation>());
        repo.Setup(invitationRepo => invitationRepo.Create(It.IsAny<Invitation>()));
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        Invitation returnedInvitation = service.Create(invitation);
        repo.VerifyAll();
        Assert.AreEqual(returnedInvitation, expectedInvitation);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidInvitationException))]
    public void CreateInvitationWithInvalidDeadLineThrowsException()
    {
        Invitation invitation = new Invitation() {CreatorId = 2, DeadLine = DateTime.Now.AddDays(-2), Name = ValidName, RecipientEmail = ValidEmail, Status = ValidStatus};
        repo.Setup(invitationRepo => invitationRepo.GetAll()).Returns(new List<Invitation>());
        repo.Setup(invitationRepo => invitationRepo.Create(It.IsAny<Invitation>()));
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        Invitation returnedInvitation = service.Create(invitation);
        repo.VerifyAll();
        Assert.AreEqual(returnedInvitation, expectedInvitation);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidInvitationLogicException))]
    public void CreateInvitationForAnExistentEmailThrowsException()
    {
        List<Invitation> invitations = new List<Invitation>() { new Invitation(){RecipientEmail = ValidEmail, DeadLine = DateTime.Now.AddDays(2), Status = "Pending"}}; 
        Invitation invitation = new Invitation() {CreatorId = 2, DeadLine = DateTime.Now.AddDays(4), Name = ValidName, RecipientEmail = ValidEmail, Status = ValidStatus};
        repo.Setup(invitationRepo => invitationRepo.GetAll()).Returns(invitations);
        repo.Setup(invitationRepo => invitationRepo.Create(It.IsAny<Invitation>()));
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        Invitation returnedInvitation = service.Create(invitation);
        repo.VerifyAll();
        Assert.AreEqual(returnedInvitation, expectedInvitation);
    }
    
    [TestMethod]
    public void DeleteInvitationOk()
    {
        repo.Setup(invitationRepo => invitationRepo.GetById(It.IsAny<int>())).Returns(expectedInvitation);
        repo.Setup(invitationRepo => invitationRepo.Delete(It.IsAny<Invitation>()));
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        bool success = service.Delete(1);
        repo.VerifyAll();
        Assert.AreEqual(TrueSuccess, success);
    }
    
    [TestMethod]
    public void DeleteNonExistentInvitationReturnsFalse()
    {
        Invitation nullInvite = null;
        repo.Setup(invitationRepo => invitationRepo.GetById(It.IsAny<int>())).Returns(nullInvite);
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        bool success = service.Delete(1);
        repo.VerifyAll();
        Assert.AreEqual(FalseSuccess, success);
    }
    
    [TestMethod]
    public void DeleteAcceptedInvitationReturnsFalse()
    {
        expectedInvitation.Status = "Accepted";
        repo.Setup(invitationRepo => invitationRepo.GetById(It.IsAny<int>())).Returns(expectedInvitation);
        InvitationLogic service = new InvitationLogic(repo.Object);
        
        bool success = service.Delete(1);
        repo.VerifyAll();
        Assert.AreEqual(FalseSuccess, success);
    }
    
    [TestMethod]
    public void UserAcceptInvitationOk()
    {
        repo.Setup(invitationRepo => invitationRepo.GetById(It.IsAny<int>())).Returns(expectedInvitation);
        InvitationLogic service = new InvitationLogic(repo.Object);

        expectedInvitation.Status = AcceptedStatus;
        Invitation returnedInvitation = service.InvitationResponse(1, ValidEmail,  true);
        repo.VerifyAll();
        Assert.AreEqual(expectedInvitation, returnedInvitation);
    }
    
    [TestMethod]
    public void UserRejectInvitationOk()
    {
        repo.Setup(invitationRepo => invitationRepo.GetById(It.IsAny<int>())).Returns(expectedInvitation);
        InvitationLogic service = new InvitationLogic(repo.Object);

        expectedInvitation.Status = RejectedStatus;
        Invitation returnedInvitation = service.InvitationResponse(1, ValidEmail,  false);
        repo.VerifyAll();
        Assert.AreEqual(expectedInvitation, returnedInvitation);
    }
    
    [TestMethod]
    [ExpectedException(typeof(InvalidInvitationLogicException))]
    public void InvitationResponseWithInvalidEmailThrowsException()
    {
        Invitation nullInvitation = null;
        repo.Setup(invitationRepo => invitationRepo.GetById(It.IsAny<int>())).Returns(nullInvitation);
        InvitationLogic service = new InvitationLogic(repo.Object);

        expectedInvitation.Status = RejectedStatus;
        Invitation returnedInvitation = service.InvitationResponse(1, "",  false);
        repo.VerifyAll();
        Assert.AreEqual(expectedInvitation, returnedInvitation);
    }
}
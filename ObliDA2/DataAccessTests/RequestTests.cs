using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.Repositories;
using DataAccess;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using DataAppContext = DataAccess.AppContext;

namespace DataAccessTests;

[TestClass]
public class RequestTests
{
    private SqliteConnection _connection;
    private DataAppContext _context;
    private RequestRepository requestRepository;
    private Building newBuilding;
    private ApartmentOwner owner;
    private Manager buildingManager;
    private Apartment newApartment;
    private MaintenanceStaff staff;
    private Request newRequest;
    private const bool ElementExists = true;
    
    [TestInitialize]
    public void Initialize()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        var contextOptions = new DbContextOptionsBuilder<DataAppContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new DataAppContext(contextOptions);
        _context.Database.EnsureCreated();

        requestRepository = new RequestRepository(_context);
        buildingManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe3@gmail.com" };
        owner = new ApartmentOwner() { Name = "pepe", LastName = "perez", Email = "pepe3@gmail.com" };
        newBuilding = new Building() {Name = "name", Address = "address", CommonExpenses = 4, Longitude = 44.33, 
            Latitude = 44.22, ConstructionCompany = "Company", BuildingManager = buildingManager};
        staff = new MaintenanceStaff() { Name = "Name", LastName = "perez", Password = "Password",Email = "pepe@gmail.com",AssociatedBuilding = newBuilding};
        newApartment = new Apartment()
        {
            NumberOfBathrooms = 1, NumberOfBedrooms = 1,Floor = 1,Number = 1,
            Building = newBuilding, Terrace = true, Owner = owner
        };
        newRequest = new Request()
        {
            AssignedToMaintenance = staff,Category = new Category(){Name = "Name"},
            BuildingAssociated  = newBuilding, Department = newApartment, Description = "p"
        };
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        _context.Requests.Add(newRequest);
        _context.SaveChanges();
        newRequest.Id = 1;
        List<Request> expectedRequests = new List<Request>()
            { newRequest };
        
        List<Request> returnedRequests = requestRepository.GetAll().ToList();
        
        
        CollectionAssert.AreEqual(expectedRequests, returnedRequests);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        List<Request> expectedRequests = new List<Request>()
            { newRequest };
        
        requestRepository.Add(newRequest);
        newRequest.Id = 1;
        List<Request> returnedRequests = requestRepository.GetAll().ToList();
        
        
        CollectionAssert.AreEqual(expectedRequests, returnedRequests);
    }
    
    [TestMethod]
    public void GetByIdOk()
    {
        _context.Requests.Add(newRequest);
        Request otherRequest = new Request()
        {
            AssignedToMaintenance = staff,Category = new Category(){Name = "otherName"},
            BuildingAssociated  = newBuilding, Department = newApartment, Description = "p"
        };
        _context.Requests.Add(otherRequest);
        _context.SaveChanges();
        otherRequest.Id = 2;
        
        Request returnedRequest = requestRepository.Get(2);
        
        Assert.AreEqual(otherRequest, returnedRequest);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        _context.Requests.Add(newRequest);
        Request otherRequest = new Request()
        {
            AssignedToMaintenance = staff,Category = new Category(){Name = "otherName"},
            BuildingAssociated  = newBuilding, Department = newApartment, Description = "p"
        };
        _context.Requests.Add(otherRequest);
        requestRepository.Remove(newRequest);
        _context.SaveChanges();
        otherRequest.Id = 2;
        
        List<Request> returnedRequest = requestRepository.GetAll().ToList();
        List<Request> expectedRequest = new List<Request>() { otherRequest };
        
        CollectionAssert.AreEqual(expectedRequest, returnedRequest);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        requestRepository.Add(newRequest);

        newRequest.Id = 1;
        newRequest.Description = "description";
        newRequest.Category = new Category() { Name = "otherCategory" };
        requestRepository.Update(newRequest);
        List<Request> returnedRequest = requestRepository.GetAll().ToList();
        List<Request> expectedRequest = new List<Request>() { newRequest };
        
        CollectionAssert.AreEqual(expectedRequest, returnedRequest);
    }
    
    [TestMethod]
    public void ExistsOk()
    {
        requestRepository.Add(newRequest);


        bool success = requestRepository.Exists(1);
        
        Assert.AreEqual(ElementExists, success);
    }
}
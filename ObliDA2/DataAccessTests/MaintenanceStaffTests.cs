using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.Repositories;
using DataAccess;
using DataAppContext = DataAccess.AppContext;

namespace DataAccessTests;

[TestClass]
public class MaintenanceStaffTests
{
    private SqliteConnection _connection;
    private DataAppContext _context;
    private MaintenanceStaffRepository staffRepository;
    
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

        staffRepository = new MaintenanceStaffRepository(_context);
    }
    
    [TestMethod]
    public void CreateAndGetAllOk()
    {
        Manager buildingManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe3@gmail.com" };
        Building buildingAssociated = new Building() {Name = "name", Address = "address", CommonExpenses = 4, Longitude = 44.33, 
            Latitude = 44.22, ConstructionCompany = "Company", BuildingManager = buildingManager};
        MaintenanceStaff newWorker = new MaintenanceStaff{ Name = "pepe", LastName = "suarez", Password = "password", Email = "pepe@gmail.com",AssociatedBuilding = buildingAssociated};
        staffRepository.Create(newWorker);
        _context.SaveChanges();
        
        List<MaintenanceStaff> returnedStaff = staffRepository.GetAll();
        List<MaintenanceStaff> expectedStaff = new List<MaintenanceStaff>()
            { new MaintenanceStaff() { Id = 1, Name = "pepe", LastName = "suarez", Password = "password", Email = "pepe@gmail.com",AssociatedBuilding = buildingAssociated } };
        
        CollectionAssert.AreEqual(expectedStaff, returnedStaff);
    }
    
    [TestMethod]
    public void GetOk()
    {
        Manager buildingManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe3@gmail.com" };
        Building buildingAssociated = new Building() {Name = "name", Address = "address", CommonExpenses = 4, Longitude = 44.33, 
            Latitude = 44.22, ConstructionCompany = "Company", BuildingManager = buildingManager};
        MaintenanceStaff newWorker = new MaintenanceStaff{ Name = "pepe", LastName = "suarez", Password = "password", Email = "pepe@gmail.com",AssociatedBuilding = buildingAssociated};
        staffRepository.Create(newWorker);
        _context.SaveChanges();
        
        MaintenanceStaff returnedStaff = staffRepository.GetById(1);
        newWorker.Id = 1;
        Assert.AreEqual(newWorker, returnedStaff);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Manager buildingManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe3@gmail.com" };
        Building buildingAssociated = new Building() {Name = "name", Address = "address", CommonExpenses = 4, Longitude = 44.33, 
            Latitude = 44.22, ConstructionCompany = "Company", BuildingManager = buildingManager};
        MaintenanceStaff newWorker = new MaintenanceStaff{ Name = "pepe", LastName = "suarez", Password = "password", Email = "pepe@gmail.com",AssociatedBuilding = buildingAssociated};
        MaintenanceStaff otherWorker = new MaintenanceStaff{ Name = "luciano", LastName = "suarez", Password = "password", Email = "other@gmail.com",AssociatedBuilding = buildingAssociated};
        staffRepository.Create(newWorker);
        _context.SaveChanges();
        staffRepository.Create(otherWorker);
        staffRepository.Delete(newWorker);
        _context.SaveChanges();
        
        
        List<MaintenanceStaff> returnedStaff = staffRepository.GetAll();
        List<MaintenanceStaff> expectedStaff = new List<MaintenanceStaff>()
            { new MaintenanceStaff() { Id = 2, Name = "luciano", LastName = "suarez", Password = "password", Email = "other@gmail.com",AssociatedBuilding = buildingAssociated } };
        
        CollectionAssert.AreEqual(expectedStaff, returnedStaff);
    }
}
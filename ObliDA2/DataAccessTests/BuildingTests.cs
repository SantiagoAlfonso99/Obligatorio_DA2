using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.Repositories;
using DataAccess;
using DataAppContext = DataAccess.AppContext;

namespace DataAccessTests;

[TestClass]
public class BuildingTests
{
    private SqliteConnection _connection;
    private DataAppContext _context;
    private BuildingRepository buildingRepository;
    
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
        
        buildingRepository = new BuildingRepository(_context);
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        Manager buildingManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe3@gmail.com" };
        Building newBuilding = new Building() {Name = "name", Address = "address", CommonExpenses = 4, Longitude = 44.33, 
            Latitude = 44.22, ConstructionCompany = "Company", BuildingManager = buildingManager};
        _context.Buildings.Add(newBuilding);
        _context.SaveChanges();
        newBuilding.Id = 1;
        List<Building> expectedBuildings = new List<Building>()
            { newBuilding };
        
        List<Building> returnedBuildings = buildingRepository.GetAll();
        
        CollectionAssert.AreEqual(expectedBuildings, returnedBuildings);
    }
    
    [TestMethod]
    public void GetOk()
    {
        Manager buildingManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe3@gmail.com" };
        Building newBuilding = new Building() {Name = "name", Address = "address", CommonExpenses = 4, Longitude = 44.33, 
            Latitude = 44.22, ConstructionCompany = "Company", BuildingManager = buildingManager};
        _context.Buildings.Add(newBuilding);
        _context.SaveChanges();
        Building otherBuilding = new Building() {Name = "otherName", Address = "address", CommonExpenses = 4, Longitude = 44.33, 
            Latitude = 44.22, ConstructionCompany = "Company", BuildingManager = buildingManager};
        _context.Buildings.Add(otherBuilding);
        _context.SaveChanges();
        otherBuilding.Id = 2;
        
        Building returnedBuilding = buildingRepository.GetById(2);
        
        Assert.AreEqual(otherBuilding, returnedBuilding);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        Manager buildingManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe3@gmail.com" };
        Building newBuilding = new Building() {Name = "name", Address = "address", CommonExpenses = 4, Longitude = 44.33, 
            Latitude = 44.22, ConstructionCompany = "Company", BuildingManager = buildingManager};
        buildingRepository.Create(newBuilding);
        _context.SaveChanges();
        newBuilding.Id = 1;
        
        Building returnedBuilding = buildingRepository.GetById(1);
        
        Assert.AreEqual(newBuilding, returnedBuilding);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Manager buildingManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe3@gmail.com" };
        Building newBuilding = new Building() {Name = "name", Address = "address", CommonExpenses = 4, Longitude = 44.33, 
            Latitude = 44.22, ConstructionCompany = "Company", BuildingManager = buildingManager};
        buildingRepository.Create(newBuilding);
        _context.SaveChanges();
        Building otherBuilding = new Building() {Name = "otherName", Address = "address", CommonExpenses = 4, Longitude = 44.33, 
            Latitude = 44.22, ConstructionCompany = "Company", BuildingManager = buildingManager};
        buildingRepository.Create(otherBuilding);
        
        newBuilding.Id = 1;
        buildingRepository.Delete(newBuilding);
        _context.SaveChanges();
        otherBuilding.Id = 2;
        List<Building> returnedBuildings = buildingRepository.GetAll();
        List<Building> expectedBuildings = new List<Building>() { otherBuilding };
        
        CollectionAssert.AreEqual(expectedBuildings, returnedBuildings);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        Manager buildingManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe3@gmail.com" };
        Building newBuilding = new Building() { Name = "name", Address = "address", CommonExpenses = 4, Longitude = 44.33, 
            Latitude = 44.22, ConstructionCompany = "Company", BuildingManager = buildingManager };
        buildingRepository.Create(newBuilding);
        _context.SaveChanges();

        newBuilding.Name = "otherName";
        newBuilding.Address = "otherAddress";
        newBuilding.CommonExpenses = 5;
        newBuilding.Longitude = 55.33;
        newBuilding.Latitude = 54.22;

        buildingRepository.Update(newBuilding);
        _context.SaveChanges();
    
        Building returnedBuilding = buildingRepository.GetById(1);
    
        Assert.AreEqual(newBuilding, returnedBuilding);
    }
}
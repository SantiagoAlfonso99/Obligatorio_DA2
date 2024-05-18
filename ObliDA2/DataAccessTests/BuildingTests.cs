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
    private Manager buildingManager;
    private Building newBuilding;
    private ConstructionCompany company;
    
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

        company = new ConstructionCompany() { Name = "Company" };
        buildingRepository = new BuildingRepository(_context);
        buildingManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe3@gmail.com" };
        newBuilding = new Building() {Name = "name", Address = "address", CommonExpenses = 4, Longitude = 44.33, 
            Latitude = 44.22, Company = company, BuildingManager = buildingManager};
    }
    
    [TestMethod]
    public void GetAllOk()
    {
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
        _context.Buildings.Add(newBuilding);
        _context.SaveChanges();
        Building otherBuilding = new Building() {Name = "otherName", Address = "address", CommonExpenses = 4, Longitude = 44.33, 
            Latitude = 44.22, Company = company, BuildingManager = buildingManager};
        _context.Buildings.Add(otherBuilding);
        _context.SaveChanges();
        otherBuilding.Id = 2;
        
        Building returnedBuilding = buildingRepository.GetById(2);
        
        Assert.AreEqual(otherBuilding, returnedBuilding);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        buildingRepository.Create(newBuilding);
        _context.SaveChanges();
        newBuilding.Id = 1;
        
        Building returnedBuilding = buildingRepository.GetById(1);
        
        Assert.AreEqual(newBuilding, returnedBuilding);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        buildingRepository.Create(newBuilding);
        Building otherBuilding = new Building() {Name = "otherName", Address = "address", CommonExpenses = 4, Longitude = 44.33, 
            Latitude = 44.22, Company = company, BuildingManager = buildingManager};
        buildingRepository.Create(otherBuilding);
        
        newBuilding.Id = 1;
        buildingRepository.Delete(newBuilding);
        otherBuilding.Id = 2;
        List<Building> returnedBuildings = buildingRepository.GetAll();
        List<Building> expectedBuildings = new List<Building>() { otherBuilding };
        
        CollectionAssert.AreEqual(expectedBuildings, returnedBuildings);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        buildingRepository.Create(newBuilding);
        
        newBuilding.Name = "otherName";
        newBuilding.Address = "otherAddress";
        newBuilding.CommonExpenses = 5;
        newBuilding.Longitude = 55.33;
        newBuilding.Latitude = 54.22;
        buildingRepository.Update(newBuilding);
    
        Building returnedBuilding = buildingRepository.GetById(1);
    
        Assert.AreEqual(newBuilding, returnedBuilding);
    }
}
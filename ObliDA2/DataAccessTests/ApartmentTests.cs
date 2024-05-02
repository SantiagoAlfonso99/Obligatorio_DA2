using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.Repositories;
using DataAccess;
using DataAppContext = DataAccess.AppContext;

namespace DataAccessTests;

[TestClass]
public class ApartmentTests
{
    private SqliteConnection _connection;
    private DataAppContext _context;
    private ApartmentRepository apartmentRepository;
    private Manager buildingManager;
    private ApartmentOwner owner;
    private Building newBuilding;
    private Apartment newApartment;
    
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

        apartmentRepository = new ApartmentRepository(_context);    
        buildingManager = new Manager() { Name = "pepe", Password = "password", Email = "pepe3@gmail.com" };
        owner = new ApartmentOwner() { Name = "pepe", LastName = "perez", Email = "pepe3@gmail.com" };
        newBuilding = new Building() {Name = "name", Address = "address", CommonExpenses = 4, Longitude = 44.33, 
            Latitude = 44.22, ConstructionCompany = "Company", BuildingManager = buildingManager};
        newApartment = new Apartment()
        {
            NumberOfBathrooms = 1, NumberOfBedrooms = 1,Floor = 1,Number = 1,
            Building = newBuilding, Terrace = true, Owner = owner
        };
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        _context.Apartments.Add(newApartment);
        _context.SaveChanges();
        newApartment.Id = 1;
        List<Apartment> expectedApartments = new List<Apartment>() { newApartment };
            
        List<Apartment> returnedApartments = apartmentRepository.GetAll();
        
        CollectionAssert.AreEqual(expectedApartments, returnedApartments);
    }
    
    [TestMethod]
    public void GetOk()
    {
        _context.Apartments.Add(newApartment);
        _context.SaveChanges();
        newApartment.Id = 1;
            
        Apartment returnedApartment = apartmentRepository.GetById(1);
        
        Assert.AreEqual(newApartment, returnedApartment);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        apartmentRepository.Create(newApartment);
        _context.SaveChanges();
        newApartment.Id = 1;
            
        Apartment returnedApartment = apartmentRepository.GetById(1);
        
        Assert.AreEqual(newApartment, returnedApartment);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        apartmentRepository.Create(newApartment);
        _context.SaveChanges();
        Apartment otherApartment = new Apartment()
        {
            NumberOfBathrooms = 2, NumberOfBedrooms = 2,Floor = 2,Number = 2,
            Building = newBuilding, Terrace = true, Owner = owner
        };
        apartmentRepository.Create(otherApartment);
        
        apartmentRepository.Delete(newApartment);
        _context.SaveChanges();
        otherApartment.Id = 2;
        List<Apartment> expectedApartments = new List<Apartment>() { otherApartment };

        List<Apartment> returnedApartments = apartmentRepository.GetAll();
        CollectionAssert.AreEqual(expectedApartments, returnedApartments);
    }
}
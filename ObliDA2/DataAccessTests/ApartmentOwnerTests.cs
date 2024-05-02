using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.Repositories;
using DataAccess;
using DataAppContext = DataAccess.AppContext;

namespace DataAccessTests;

[TestClass]
public class ApartmentOwnerTests
{
    private SqliteConnection _connection;
    private DataAppContext _context;
    private ApartmentOwnerRepository ownersRepository;
    private ApartmentOwner newOwner;
    private const int OwnerId = 1;
    
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
        
        ownersRepository = new ApartmentOwnerRepository(_context);
        newOwner = new ApartmentOwner() { Name = "pepe2", LastName = "suarez", Email = "pepe2@gmail.com"};
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        _context.Owners.Add(newOwner);
        _context.SaveChanges();
        newOwner.Id = OwnerId;
        List<ApartmentOwner> expectedOwners = new List<ApartmentOwner>()
            { newOwner};
        
        List<ApartmentOwner> returnedOwners = ownersRepository.GetAll();
        
        CollectionAssert.AreEqual(expectedOwners, returnedOwners);
    }
    
    [TestMethod]
    public void GetByIdOk()
    {
        _context.Owners.Add(newOwner);
        _context.SaveChanges();
        newOwner.Id = OwnerId;
        
        ApartmentOwner returnedOwner = ownersRepository.GetById(OwnerId);
        
        Assert.AreEqual(newOwner, returnedOwner);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        ownersRepository.Create(newOwner);
        _context.SaveChanges();
        newOwner.Id = OwnerId;
        List<ApartmentOwner> expectedOwners = new List<ApartmentOwner>()
            { newOwner};
        
        List<ApartmentOwner> returnedOwners = ownersRepository.GetAll();
        
        
        CollectionAssert.AreEqual(expectedOwners, returnedOwners);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        ownersRepository.Create(newOwner);
        _context.SaveChanges();
        newOwner.Name = "raul";
        newOwner.LastName = "cavani";
        ownersRepository.Update(newOwner);
        _context.SaveChanges();
        List<ApartmentOwner> expectedOwners = new List<ApartmentOwner>()
            { newOwner};
        
        List<ApartmentOwner> returnedOwners = ownersRepository.GetAll();
        
        CollectionAssert.AreEqual(expectedOwners, returnedOwners);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        ApartmentOwner otherOwner = new ApartmentOwner() { Name = "raul", LastName = "suarez", Email = "raul@gmail.com"};
        ownersRepository.Create(newOwner);
        _context.SaveChanges();
        ownersRepository.Create(otherOwner);
        ownersRepository.Delete(newOwner);
        _context.SaveChanges();
        otherOwner.Id = 2;
        List<ApartmentOwner> expectedOwners = new List<ApartmentOwner>()
            { otherOwner};
        
        List<ApartmentOwner> returnedOwners = ownersRepository.GetAll();
        
        CollectionAssert.AreEqual(expectedOwners, returnedOwners);
    }
}
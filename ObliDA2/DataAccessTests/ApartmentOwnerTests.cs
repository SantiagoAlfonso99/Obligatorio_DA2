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
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        ApartmentOwner newOwner = new ApartmentOwner() { Name = "pepe2", LastName = "suarez", Email = "pepe2@gmail.com"};
        _context.Owners.Add(newOwner);
        _context.SaveChanges();
        newOwner.Id = 1;
        
        List<ApartmentOwner> returnedOwners = ownersRepository.GetAll();
        List<ApartmentOwner> expectedOwners = new List<ApartmentOwner>()
            { newOwner};
        
        CollectionAssert.AreEqual(expectedOwners, returnedOwners);
    }
    
    [TestMethod]
    public void GetByIdOk()
    {
        ApartmentOwner newOwner = new ApartmentOwner() { Name = "pepe2", LastName = "suarez", Email = "pepe2@gmail.com"};
        _context.Owners.Add(newOwner);
        _context.SaveChanges();
        newOwner.Id = 1;
        
        ApartmentOwner returnedOwner = ownersRepository.GetById(1);
        
        Assert.AreEqual(newOwner, returnedOwner);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        ApartmentOwner newOwner = new ApartmentOwner() { Name = "pepe2", LastName = "suarez", Email = "pepe2@gmail.com"};
        ownersRepository.Create(newOwner);
        _context.SaveChanges();
        newOwner.Id = 1;
        
        List<ApartmentOwner> returnedOwners = ownersRepository.GetAll();
        List<ApartmentOwner> expectedOwners = new List<ApartmentOwner>()
            { newOwner};
        
        CollectionAssert.AreEqual(expectedOwners, returnedOwners);
    }
    
    [TestMethod]
    public void UpdateOk()
    {
        ApartmentOwner newOwner = new ApartmentOwner() { Name = "pepe2", LastName = "suarez", Email = "pepe2@gmail.com"};
        ownersRepository.Create(newOwner);
        _context.SaveChanges();
        newOwner.Name = "raul";
        newOwner.LastName = "cavani";
        ownersRepository.Update(newOwner);
        _context.SaveChanges();
        
        List<ApartmentOwner> returnedOwners = ownersRepository.GetAll();
        List<ApartmentOwner> expectedOwners = new List<ApartmentOwner>()
            { newOwner};
        
        CollectionAssert.AreEqual(expectedOwners, returnedOwners);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        ApartmentOwner newOwner = new ApartmentOwner() { Name = "pepe2", LastName = "suarez", Email = "pepe2@gmail.com"};
        ApartmentOwner otherOwner = new ApartmentOwner() { Name = "raul", LastName = "suarez", Email = "raul@gmail.com"};
        ownersRepository.Create(newOwner);
        _context.SaveChanges();
        ownersRepository.Create(otherOwner);
        ownersRepository.Delete(newOwner);
        _context.SaveChanges();
        otherOwner.Id = 2;
        
        List<ApartmentOwner> returnedOwners = ownersRepository.GetAll();
        List<ApartmentOwner> expectedOwners = new List<ApartmentOwner>()
            { otherOwner};
        
        CollectionAssert.AreEqual(expectedOwners, returnedOwners);
    }
}
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using DataAccess.Repositories;
using DataAccess;
using DataAppContext = DataAccess.AppContext;

namespace DataAccessTests;

[TestClass]
public class CategoryTests
{
    private SqliteConnection _connection;
    private DataAppContext _context;
    private CategoryRepository categoryRepository;
    
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
        
        categoryRepository = new CategoryRepository(_context);
    }
    
    [TestMethod]
    public void GetAllOk()
    {
        Category newCategory = new Category() { Name = "Vecino Molesto" };
        _context.Categories.Add(newCategory);
        _context.SaveChanges();
        
        List<Category> returnedCategories = categoryRepository.GetAll();
        List<Category> expectedCategories = new List<Category>()
            { new Category() { Id = 1, Name = "Vecino Molesto"} };
        
        CollectionAssert.AreEqual(expectedCategories, returnedCategories);
    }
    
    [TestMethod]
    public void GetByIdOk()
    {
        Category newCategory = new Category() { Name = "Vecino Molesto" };
        _context.Categories.Add(newCategory);
        _context.SaveChanges();
        newCategory.Id = 1;
        
        Category returnedCategory = categoryRepository.GetById(1);
        
        Assert.AreEqual(newCategory, returnedCategory);
    }
    
    [TestMethod]
    public void CreateOk()
    {
        Category newCategory = new Category() { Name = "Vecino Molesto" };
        categoryRepository.Create(newCategory);
        _context.SaveChanges();
        
        List<Category> returnedCategories = categoryRepository.GetAll();
        List<Category> expectedCategories = new List<Category>()
            { new Category() { Id = 1, Name = "Vecino Molesto"} };
        
        CollectionAssert.AreEqual(expectedCategories, returnedCategories);
    }
    
    [TestMethod]
    public void DeleteOk()
    {
        Category newCategory = new Category() { Name = "Vecino Molesto" };
        Category otherCategory = new Category() { Name = "Electricista" };
        List<Category> expectedCategories = new List<Category>()
            { new Category() { Id = 2, Name = "Electricista"} };
        
        categoryRepository.Create(newCategory);
        _context.SaveChanges();
        newCategory.Id = 1;
        categoryRepository.Create(otherCategory);
        categoryRepository.Delete(newCategory);
        _context.SaveChanges();
        
        
        List<Category> returnedCategories = categoryRepository.GetAll();
        
        CollectionAssert.AreEqual(expectedCategories, returnedCategories);
    }
}
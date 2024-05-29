using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Models;

namespace DataAccess;

public class AppContext : DbContext
{
    public AppContext() { }
    public AppContext(DbContextOptions options) : base(options) { }
    
    public virtual DbSet<Admin>? Admins { get; set; }
    public virtual DbSet<Manager>? Managers { get; set; }
    public virtual DbSet<MaintenanceStaff>? MaintenancePersonnel { get; set; }
    public virtual DbSet<Building>? Buildings { get; set; }
    public virtual DbSet<Apartment>? Apartments { get; set; }
    public virtual DbSet<ApartmentOwner>? Owners { get; set; }
    public virtual DbSet<Category>? Categories { get; set; }
    public virtual DbSet<Request>? Requests { get; set; }
    public virtual DbSet<Invitation>? Invitations { get; set; }
    public virtual DbSet<Session>? Sessions { get; set; }
    public virtual DbSet<CompanyAdmin>? CompanyAdmins { get; set; }
    public virtual DbSet<ConstructionCompany>? ConstructionCompanies { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>().ToTable("Admin");
        modelBuilder.Entity<Manager>().ToTable("Manager");
        modelBuilder.Entity<MaintenanceStaff>().ToTable("MaintenancePersonnel");
        modelBuilder.Entity<CompanyAdmin>().ToTable("CompanyAdmin");

        modelBuilder.Entity<MaintenanceStaff>()
            .HasMany(m => m.Buildings)
            .WithMany(b => b.Workers);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string directory = Directory.GetCurrentDirectory();

            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(directory)
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configuration.GetConnectionString(@"obliDA2DB");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
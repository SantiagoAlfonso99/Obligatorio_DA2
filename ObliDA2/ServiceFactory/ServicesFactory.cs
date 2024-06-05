using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IBusinessLogic;
using BusinessLogic.Services;
using Domain.Models;
using DataAccess.Repositories;
using BusinessLogic.IRepository;
using IImporter;

namespace ServiceFactory;

public class ServicesFactory
{
    public ServicesFactory() { }

    public static void RegisterServices(IServiceCollection serviceCollection){
        serviceCollection.AddTransient<IAdminLogic, AdminLogic>();
        serviceCollection.AddTransient<IManagerLogic, ManagerLogic>();
        serviceCollection.AddTransient<ICompanyAdminLogic, CompanyAdminLogic>();
        serviceCollection.AddTransient<IMaintenanceLogic, MaintenanceStaffLogic>();
        serviceCollection.AddTransient<IApartmentLogic, ApartmentLogic>();
        serviceCollection.AddTransient<IApartmentOwnerLogic, ApartmentOwnerLogic>();
        serviceCollection.AddTransient<ICategoryLogic, CategoryLogic>();
        serviceCollection.AddTransient<IBuildingLogic, BuildingLogic>();
        serviceCollection.AddTransient<IInvitationLogic, InvitationLogic>();
        serviceCollection.AddTransient<IReportLogic, ReportLogic>();
        serviceCollection.AddTransient<IImporterLogic, ImporterLogic>();
        
        serviceCollection.AddScoped<IUsersLogic, UsersLogic>();
    }
    
    public static void RegisterReportService(IServiceCollection serviceCollection){
        serviceCollection.AddTransient<IReportLogic>(serviceProvider =>
        {
            var managerLogic = serviceProvider.GetRequiredService<IManagerLogic>();
            var buildingLogic = serviceProvider.GetRequiredService<IBuildingLogic>();
            var maintenanceStaffLogic = serviceProvider.GetRequiredService<IMaintenanceLogic>();
            
            return new ReportLogic(managerLogic, buildingLogic, maintenanceStaffLogic);
        });
    }
    
    public static void RegisterDataAccess(IServiceCollection serviceCollection){
        serviceCollection.AddDbContext<DbContext, DataAccess.AppContext>();
        serviceCollection.AddTransient<IAdminRepository, AdminRepository>();
        serviceCollection.AddTransient<IManagerRepository, ManagerRepository>();
        serviceCollection.AddTransient<IMaintenanceStaffRepository, MaintenanceStaffRepository>();
        serviceCollection.AddTransient<IApartmentRepository, ApartmentRepository>();
        serviceCollection.AddTransient<IApartmentOwnerRepository, ApartmentOwnerRepository>();
        serviceCollection.AddTransient<ICategoryRepository, CategoryRepository>();
        serviceCollection.AddTransient<IBuildingRepository, BuildingRepository>();
        serviceCollection.AddTransient<IRequestRepository, RequestRepository>();
        serviceCollection.AddTransient<ISessionRepository, SessionRepository>();
        serviceCollection.AddTransient<IConstructionCompanyRepository, ConstructionCompanyRepository>();
        serviceCollection.AddTransient<ICompanyAdminRepository, CompanyAdminRepository>();
        serviceCollection.AddTransient<IInvitationRepository, InvitationRepository>();
    }

    public static void CreateDefaultUser(IServiceProvider serviceProvider)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            var userLogic = services.GetRequiredService<IUsersLogic>();
            var adminLogic = services.GetRequiredService<IAdminLogic>();
            userLogic.ValidateEmail("admin@example.com");
            var defaultUser = new Admin
            {
                Name = "raul",
                Email = "admin@example.com",
                Password = "Perez123",
                LastName = "Perez"
            };
            adminLogic.Create(defaultUser);
        }
        catch (Exception)
        {
            return;
        }
    }
    
    public static void CreateDefaultCategories(IServiceProvider serviceProvider)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            
            var categoryLogic = services.GetRequiredService<ICategoryLogic>();
            categoryLogic.Create(new Category() { Name = "Electricista" });
            categoryLogic.Create(new Category() { Name = "Fontanero" });
            categoryLogic.Create(new Category() { Name = "Plomero" });
            categoryLogic.Create(new Category() { Name = "Albañil" });
            categoryLogic.Create(new Category() { Name = "Vecino Molesto"});
        }
        catch (Exception)
        {
            return;
        }
    }
}
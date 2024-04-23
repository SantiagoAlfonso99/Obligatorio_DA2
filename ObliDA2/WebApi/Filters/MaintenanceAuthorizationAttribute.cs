namespace WebApi.Filters;

public class MaintenanceAuthorizationAttribute : BaseAuthorizationAttribute
{
    public MaintenanceAuthorizationAttribute() : base("maintenance") {}
}
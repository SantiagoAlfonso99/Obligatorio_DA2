namespace WebApi.Filters;

public class AdminAuthorizationAttribute : BaseAuthorizationAttribute
{
    public AdminAuthorizationAttribute() : base("admin") {}
}
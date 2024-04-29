namespace WebApi.Filters;

public class ManagerAuthorizationAttribute : BaseAuthorizationAttribute
{
    public ManagerAuthorizationAttribute() : base("manager") {}
}
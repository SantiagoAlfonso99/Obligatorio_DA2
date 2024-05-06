using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Domain.Exceptions;

namespace WebApi.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        try
        {
            throw context.Exception;
        }
        catch (NotFoundException e)
        {
            context.Result = new JsonResult("The operation could not be performed because the element was not found.") { StatusCode = 404 };
        }
        catch (EmptyOrNullException e)
        {
            context.Result = new JsonResult("Make sure not to enter empty or null values.") { StatusCode = 400 };
        }
        catch (ArgumentException e)
        {
            context.Result = new JsonResult("Make sure to enter valid values") { StatusCode = 400 };
        }
        catch (InvalidOperationException e)
        {
            context.Result = new JsonResult("An invalid operation occurred:") { StatusCode = 409 };
        }
        /*catch (Exception)
        {
            context.Result = new JsonResult("We encountered some issues, try again later") { StatusCode = 500 };
        }*/
    }
}
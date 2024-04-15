using Addresso.Api.Repositories;
using Addresso.UI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Addresso.Api.Filters.Contacts;

public class Contact_ValidateContactToCreateFilterAttribute(IContactRepository repository) : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        Contact? incomingContact = context.ActionArguments["contact"] as Contact;

        if (incomingContact == null)
        {
            context.ModelState.AddModelError("Contact Null", "Contact is null.");
            ValidationProblemDetails problemDetail = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetail);
        }
        else
        {
            if(repository.Exists(incomingContact))
            {
                context.ModelState.AddModelError("Contact Exists", "New Contact already exists");
                ValidationProblemDetails problemDetail = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetail);
            }
        }
    }
}
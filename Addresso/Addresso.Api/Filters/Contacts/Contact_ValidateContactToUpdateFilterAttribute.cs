using Addresso.Api.Repositories;
using Addresso.UI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Addresso.Api.Filters.Contacts;

public class Contact_ValidateContactToUpdateFilterAttribute(IContactRepository repository) : ActionFilterAttribute
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
            if(!repository.Exists(incomingContact.Id))
            {
                context.ModelState.AddModelError("Contact Not Found", "Contact cannot be found.");
                ValidationProblemDetails problemDetail = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status404NotFound
                };
                context.Result = new NotFoundObjectResult(problemDetail);
            }
        }
    }
}
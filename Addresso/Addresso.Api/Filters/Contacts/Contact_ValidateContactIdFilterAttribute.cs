using Addresso.Api.Repositories;
using Addresso.UI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Addresso.Api.Filters.Contacts;

public class Contact_ValidateContactIdFilterAttribute(IContactRepository repository) : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        int? incomingContactId = context.ActionArguments["id"] as int?;

        if (incomingContactId == null)
        {
            context.ModelState.AddModelError("Contact Id Null", "Contact Id is null.");
            ValidationProblemDetails problemDetail = new ValidationProblemDetails(context.ModelState)
            {
                Status = StatusCodes.Status400BadRequest
            };
            context.Result = new BadRequestObjectResult(problemDetail);
        }
        else
        {
            if(incomingContactId.Value < 1)
            {
                context.ModelState.AddModelError("Contact Id Less Than 0", "Contact Id is less than 0.");
                ValidationProblemDetails problemDetail = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetail);
            }
            else
            {
                if (!repository.Exists(incomingContactId.Value))
                {
                    context.ModelState.AddModelError("Contact Id Not Found", "Contact Id does not exist.");
                    ValidationProblemDetails problemDetail = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new NotFoundObjectResult(problemDetail);
                }
            }
        }
    }
}
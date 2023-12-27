using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OrganizacnaStrukturaFirmy.Controllers.Filters.ActionFilters
{
    public class Employee_ValidateLevelFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var level = context.ActionArguments["level"] as int?;
            if (level > 4 || level < 1)
            {
                context.ModelState.AddModelError("Level", "Level has to be from 1 to 4");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Controllers.Filters.ActionFilters
{
    public class Employee_ValidateEmployeeFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var id = context.ActionArguments["id"] as int?;
            var employee = context.ActionArguments["employee"] as Employee;

            if (id.HasValue && employee != null && id != employee.Id)
            {
                context.ModelState.AddModelError("EmployeeId", "EmployeeId is not the same as id");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
        }
    }
}
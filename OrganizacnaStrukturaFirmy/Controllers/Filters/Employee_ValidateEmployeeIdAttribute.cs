using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrganizacnaStrukturaFirmy.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizacnaStrukturaFirmy.Controllers.Filters
{
    public class Employee_ValidateEmployeeIdAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var employeeId = context.ActionArguments["id"] as int?;
            var dbContext = (DataContext)context.(typeof(DataContext));
            if (employeeId.HasValue)
            {
                if (employeeId.Value <= 0)
                {
                    context.ModelState.AddModelError("EmployeeId", "EmployeeId is invalid");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else if (//check if db table nodes has node with given id)

            }
        }
    }
}

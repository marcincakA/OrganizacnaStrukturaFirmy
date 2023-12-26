using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrganizacnaStrukturaFirmy.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizacnaStrukturaFirmy.Controllers.Filters
{
    public class Employee_ValidateEmployeeIdAttribute : ActionFilterAttribute
    {
        private readonly DataContext _dbContext;

        public Employee_ValidateEmployeeIdAttribute(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var employeeId = context.ActionArguments["id"] as int?;
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
                else
                {
                    var employeeExists = _dbContext.Employees.Any(e => e.Id == employeeId.Value);
                    if (!employeeExists)
                    {
                        context.ModelState.AddModelError("EmployeeId", "Employee with given id does not exist");
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status404NotFound
                        };
                        context.Result = new BadRequestObjectResult(problemDetails);
                    }
                }

            }
        }
    }
}

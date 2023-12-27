using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrganizacnaStrukturaFirmy.Data;
using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Controllers.Filters.ActionFilters
{
    public class Employee_ValidateWorkplaceIdExistanceFilterAttribute : ActionFilterAttribute
    {
        private readonly DataContext _dbContext;

        public Employee_ValidateWorkplaceIdExistanceFilterAttribute(DataContext context)
        {
            _dbContext = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var employee = context.ActionArguments["employee"] as Employee;
            if (employee != null)
            {
                if (employee.Id_workplace <= 0)
                {
                    context.ModelState.AddModelError("Id_workplace", "Id_workplace is invalid");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else
                {
                    if (!_dbContext.Nodes.Any(n => n.Id == employee.Id_workplace))
                    {
                        context.ModelState.AddModelError("Id_workplace", "Workplace does not exist");
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

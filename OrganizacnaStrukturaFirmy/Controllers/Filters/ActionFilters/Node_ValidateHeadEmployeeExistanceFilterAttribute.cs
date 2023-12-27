using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrganizacnaStrukturaFirmy.Data;
using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Controllers.Filters.ActionFilters
{
    public class Node_ValidateHeadEmployeeExistanceFilterAttribute : ActionFilterAttribute
    {
        private readonly DataContext _dbContext;

        public Node_ValidateHeadEmployeeExistanceFilterAttribute(DataContext context)
        {
            _dbContext = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var node = context.ActionArguments["node"] as Node;
            if (node != null)
            {
                if (node.Id_headEmployee <= 0)
                {
                    context.ModelState.AddModelError("Id_headEmployee", "Id_headEmployee is invalid");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }
            else // mozno stacil else if
            {
                if (!_dbContext.Employees.Any(e => e.Id == node.Id_headEmployee))
                {
                    context.ModelState.AddModelError("Id_headEmployee", "Employee with given id does not exist");
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

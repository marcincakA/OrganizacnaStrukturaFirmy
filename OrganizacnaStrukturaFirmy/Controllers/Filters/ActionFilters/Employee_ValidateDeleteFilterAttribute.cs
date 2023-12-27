using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrganizacnaStrukturaFirmy.Data;

namespace OrganizacnaStrukturaFirmy.Controllers.Filters.ActionFilters
{
    public class Employee_ValidateDeleteFilterAttribute : ActionFilterAttribute
    {
        private readonly DataContext _dbContext;

        public Employee_ValidateDeleteFilterAttribute(DataContext dataContext)
        {
            _dbContext = dataContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var id = context.ActionArguments["id"] as int?;
            if (id != null)
            {
                var employee = _dbContext.Employees.Find(id);
                if (_dbContext.Nodes.Any(n => n.Id_headEmployee == employee.Id))
                {
                    var node = _dbContext.Nodes.FirstOrDefault(n => n.Id_headEmployee == employee.Id);
                    context.ModelState.AddModelError("Id", $"Can't delete given employee. Employee is still head of node {node.Id}");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrganizacnaStrukturaFirmy.Data;

namespace OrganizacnaStrukturaFirmy.Controllers.Filters.ActionFilters
{
    public class Node_ValidateDeleteFilterAttribute : ActionFilterAttribute
    {
        private readonly DataContext _dbContext;

        public Node_ValidateDeleteFilterAttribute(DataContext context)
        {
            _dbContext = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var id = context.ActionArguments["id"] as int?;
            if (id != null)
            {
                var node = _dbContext.Nodes.Find(id); // kontrolu existencie riesi iny filter
                if (_dbContext.Nodes.Any(n => n.Id_parentNode == node.Id))
                {
                    context.ModelState.AddModelError("Id", "Cant delete given node, delete subnodes first");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else if (_dbContext.Employees.Any(e => e.Id_workplace == id))
                {
                    context.ModelState.AddModelError("Id", "Cant delete given node, there are still employees assigned to it");
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

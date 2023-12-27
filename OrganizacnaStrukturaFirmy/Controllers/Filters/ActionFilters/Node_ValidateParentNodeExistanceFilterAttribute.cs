using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrganizacnaStrukturaFirmy.Data;
using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Controllers.Filters.ActionFilters
{
    public class Node_ValidateParentNodeExistanceFilterAttribute : ActionFilterAttribute
    {
        private readonly DataContext _dbContext;
        public Node_ValidateParentNodeExistanceFilterAttribute(DataContext context)
        {
            _dbContext = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var node = context.ActionArguments["node"] as Node;
            if (node == null)
            {
                if (node.Id_parentNode <= 0)
                {
                    context.ModelState.AddModelError("Id_parentNode", "Id_parentNode is incorrect");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else
                {
                    if (!_dbContext.Nodes.Any(n => n.Id == node.Id_parentNode))
                    {
                        context.ModelState.AddModelError("Id_parentNode", "Parent node with given id does not exist");
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
}

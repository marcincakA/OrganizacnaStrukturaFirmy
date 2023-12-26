using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrganizacnaStrukturaFirmy.Data;

namespace OrganizacnaStrukturaFirmy.Controllers.Filters.ActionFilters
{
    public class Node_ValidateNodeIdAttribute : ActionFilterAttribute
    {
        private readonly DataContext _dataContext;

        public Node_ValidateNodeIdAttribute(DataContext context)
        {
            _dataContext = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var nodeId = context.ActionArguments["id"] as int?;

            if (nodeId.HasValue)
            {
                if (nodeId.Value <= 0)
                {
                    context.ModelState.AddModelError("NodeId", "NodeID is invalid.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else
                {
                    var nodeExists = _dataContext.Nodes.Any(n => n.Id == nodeId);
                    if (!nodeExists)
                    {
                        context.ModelState.AddModelError("NodeId", "Node with given id does not exist");
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

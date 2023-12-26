using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrganizacnaStrukturaFirmy.Data;

namespace OrganizacnaStrukturaFirmy.Controllers.Filters.ExceptionFilters
{
    public class Node_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        private readonly DataContext _dataContext;

        public Node_HandleUpdateExceptionsFilterAttribute(DataContext data)
        {
            _dataContext = data;
        }
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strNodeId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strNodeId, out int nodeId))
            {
                if (!_dataContext.Nodes.Any(n => n.Id == nodeId))
                {
                    context.ModelState.AddModelError("NodeId", "Node doesn't exist anymore.");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status404NotFound
                    };
                    context.Result = new NotFoundObjectResult(problemDetails);
                }
            }
        }
    }
}

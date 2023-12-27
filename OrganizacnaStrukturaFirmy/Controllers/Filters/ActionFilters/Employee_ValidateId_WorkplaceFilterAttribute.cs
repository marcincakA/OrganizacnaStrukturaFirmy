using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrganizacnaStrukturaFirmy.Data;

namespace OrganizacnaStrukturaFirmy.Controllers.Filters.ActionFilters
{

    public class Employee_ValidateId_WorkplaceFilterAttribute : ActionFilterAttribute
    {
        private readonly DataContext _dbContext;

        public Employee_ValidateId_WorkplaceFilterAttribute(DataContext context)
        {
            _dbContext = context;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var id = context.ActionArguments["Id_workplace"] as int?;
            if (id != null)
            {
                if (id <= 0)
                {
                    context.ModelState.AddModelError("Id_workplace", "Id_workplace is incorrect");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
                else if (!_dbContext.Nodes.Any(n => n.Id == id))
                {
                    context.ModelState.AddModelError("Id_workplace", "There is no workplace with given id");
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

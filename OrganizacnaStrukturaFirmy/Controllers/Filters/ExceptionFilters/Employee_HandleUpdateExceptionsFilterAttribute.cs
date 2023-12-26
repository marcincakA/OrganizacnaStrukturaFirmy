using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using OrganizacnaStrukturaFirmy.Data;

namespace OrganizacnaStrukturaFirmy.Controllers.Filters.ExceptionFilters
{
    public class Employee_HandleUpdateExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        private readonly DataContext _dataContext;

        public Employee_HandleUpdateExceptionsFilterAttribute(DataContext data)
        {
            _dataContext = data;
        }
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strEmployeeId = context.RouteData.Values["id"] as string;
            if (int.TryParse(strEmployeeId, out int employeeId))
            {
                if (!_dataContext.Employees.Any(e => e.Id == employeeId))
                {
                    context.ModelState.AddModelError("EmployeeId", "Employee doesn't exist anymore.");
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

using System.ComponentModel.DataAnnotations;
using OrganizacnaStrukturaFirmy.Data;

namespace OrganizacnaStrukturaFirmy.Models.Validations
{
    public class Employee_UniqueEmailAttribute : ValidationAttribute
    {
        private readonly DataContext _dbContext;
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            //var context = (DataContext)validationContext.GetService(typeof(DataContext));
            var employee = validationContext.ObjectInstance as Employee;
            if (employee != null)
            {
                var foundEmployee = _dbContext.Employees.FirstOrDefault(e => e.Email == employee.Email);
                if (foundEmployee != null)
                {
                    return new ValidationResult("Email is not unique");
                }

            }
            return ValidationResult.Success;
        }
    }
}

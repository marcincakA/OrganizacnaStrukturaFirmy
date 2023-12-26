using OrganizacnaStrukturaFirmy.Data;
using System.ComponentModel.DataAnnotations;

namespace OrganizacnaStrukturaFirmy.Models.Validations
{
    public class Node_HeadEmployeeAttribte : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = (DataContext)validationContext.GetService(typeof(DataContext));
            var node = validationContext.ObjectInstance as Node;

            if (node != null)
            {
                var foundEmployee = context.Employees.Find(node.Id_headEmployee);

                if (foundEmployee != null)
                {
                    if (foundEmployee.Id_workplace != node.Id)
                    {
                        return new ValidationResult("Employee does not work in given node");
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}

using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace OrganizacnaStrukturaFirmy.Models.Validations
{
    public class Node_LevelMaxValueAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var node = validationContext.ObjectInstance as Node;
            if (node != null)
            {
                if (node.Level <= 0 || node.Level > 4)
                {
                    return new ValidationResult("Wrong value for node level");
                }
            }
            return ValidationResult.Success;
        }
        
    }
}

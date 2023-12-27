using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using OrganizacnaStrukturaFirmy.Data;

namespace OrganizacnaStrukturaFirmy.Models.Validations
{
    public class Node_LevelAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var node = validationContext.ObjectInstance as Node;
            var context = (DataContext)validationContext.GetService(typeof(DataContext));

            if (node != null)
            {
                if (node.Id_parentNode == null) //pravdepodobne tam nula nemoze byt kedze to je foreign key -_-
                {

                        if (node.Level != 1)
                    {
                        return new ValidationResult("Company has to be level 1.");
                    }
                }

                else if (node.Id_parentNode <= 0)
                {
                    return new ValidationResult("Incorrect parrent node id");
                }

                else if (node.Id_parentNode > 0)
                {
                    var parentNode = context.Nodes.Find(node.Id_parentNode);
                    if (parentNode == null)
                    {
                        return new ValidationResult("Parent node does not exist");
                    }
                    else if (node.Level != parentNode.Level + 1)
                    {
                        return new ValidationResult("Incorrect level for given node.");
                    }
                }

                if (node.Level > 4)
                {
                    return new ValidationResult("Cant create nodes with level bigger than 4");
                }
            }
            return ValidationResult.Success;
        }
    }
}

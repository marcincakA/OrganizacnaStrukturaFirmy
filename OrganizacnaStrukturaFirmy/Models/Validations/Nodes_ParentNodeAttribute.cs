using OrganizacnaStrukturaFirmy.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using OrganizacnaStrukturaFirmy.Controllers;
using OrganizacnaStrukturaFirmy.Data;

namespace OrganizacnaStrukturaFirmy.Models.Validations
{
    public class Nodes_ParentNodeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = (DataContext)validationContext.GetService(typeof(DataContext)); 
            var Node = validationContext.ObjectInstance as Node;

            if (Node != null && !(Node.Id_parentNode is null))
            {
                
                //Look for node based on parentNode id
                //Repeat untill parentNode id is null or equal to Node id
                //count number of recursions if the number is bigger than 4 return  new validationResult
                if ((RecursiveCheck(context, Node, Node.Level)) is false)
                {
                    return new ValidationResult($"Wrong level for given node");
                }
            }
            return ValidationResult.Success;
        }

        private bool RecursiveCheck(DataContext context, Node node, int maxRecursionLevels)
        {
            if (maxRecursionLevels <= 0)
            {
                return false; //failed validation
            }

            if (node.Id_parentNode is null || node.Id_parentNode == node.Id)
            {
                return true;
            }

            var parentNode = context.Nodes.Find(node.Id_parentNode);
            if (parentNode != null)
            {
                return RecursiveCheck(context, parentNode, maxRecursionLevels - 1);
            }

            return false;
        }
    }

    
}

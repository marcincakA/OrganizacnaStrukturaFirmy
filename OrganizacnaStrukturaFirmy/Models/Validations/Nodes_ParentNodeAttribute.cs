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
                //count number of recursions if the number is bigger than 3 return  new validationResult
            }
        }
    }
}

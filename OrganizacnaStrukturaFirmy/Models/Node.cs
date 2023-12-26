using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using OrganizacnaStrukturaFirmy.Models.Validations;

namespace OrganizacnaStrukturaFirmy.Models
{
    public class Node
    {
        public int Id { get; set; }
        
        [Required]
        public string NodeName { get; set; }
        
        [Required]
        public string NodeCode { get; set; }

        [Nodes_ParentNode]
        public int? Id_parentNode { get; set; }

        [Node_HeadEmployeeAttribte]
        public int? Id_headEmployee { get; set; }

        [Required]
        [Node_LevelAttribute]
        public int Level { get; set; }
    }
}

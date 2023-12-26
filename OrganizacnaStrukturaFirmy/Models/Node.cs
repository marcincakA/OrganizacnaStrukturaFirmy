using System.ComponentModel.DataAnnotations;
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

        public int? Id_headEmployee { get; set; }

        public int Level { get; set; }
    }
}

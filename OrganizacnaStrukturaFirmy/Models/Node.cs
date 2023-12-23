using System.ComponentModel.DataAnnotations;

namespace OrganizacnaStrukturaFirmy.Models
{
    public class Node
    {
        public int Id { get; set; }
        
        [Required]
        public string NodeName { get; set; }
        
        [Required]
        public string NodeCode { get; set; }


        public int? Id_parentNode { get; set; }

        public int? Id_headEmployee { get; set; }
    }
}

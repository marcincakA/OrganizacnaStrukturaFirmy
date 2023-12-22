using System.ComponentModel.DataAnnotations;

namespace OrganizacnaStrukturaFirmy.Models
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string? Title { get; set; }
        public int? Id_workplace { get; set; } = 0;
    }
}

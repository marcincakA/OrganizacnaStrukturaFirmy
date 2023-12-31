﻿using System.ComponentModel.DataAnnotations;
using OrganizacnaStrukturaFirmy.Models.Validations;

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

        public int? Id_workplace { get; set; } = null;

        [Required]
        [Phone]
        [Employee_UniquePhone] // ci to bol dobry napad kontrolovat tu a nie v controlleri, neviem asi skor nie ako hej
        public string Phone { get; set; }

        [Required]
        [EmailAddress]
        [Employee_UniqueEmail]
        public string Email { get; set; }
    }
}

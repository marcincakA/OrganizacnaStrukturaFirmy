﻿using System.ComponentModel.DataAnnotations;
using OrganizacnaStrukturaFirmy.Data;

namespace OrganizacnaStrukturaFirmy.Models.Validations
{
    public class Employee_UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var context = (DataContext)validationContext.GetService(typeof(DataContext));
            var employee = validationContext.ObjectInstance as Employee;
            if (employee != null)
            {
                var foundEmployee = context.Employees.FirstOrDefault(e => e.Email == employee.Email && e.Id != employee.Id);
                if (foundEmployee != null)
                {
                    return new ValidationResult("Email is not unique");
                }

            }
            return ValidationResult.Success;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizacnaStrukturaFirmy.Data;
using OrganizacnaStrukturaFirmy.Models;

namespace OrganizacnaStrukturaFirmy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly DataContext _context;

        public EmployeeController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> addEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return Ok(await _context.Employees.ToListAsync());
        }

        [HttpGet("id")]
        public async Task<ActionResult<Employee>> getEmployeeById(int id)
        {
            //ToDO validacia vo filtry
            var Employee = await _context.Employees.FindAsync(id);

            if (Employee is null)
            {
                return NotFound("Employee not found");
            }
            return Ok(Employee);
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> getEmployees()
        {
            var heroes = await _context.Employees.ToListAsync();
            return Ok(heroes);
        }

        [HttpDelete("id")]
        public async Task<ActionResult<Employee>> deleteEmployee(int id)
        {
            //Todo validacia id filter
            var Employee = await _context.Employees.FindAsync(id);

            if (Employee is null)
            {
                return NotFound("Employee not found");
            }

            _context.Employees.Remove(Employee);
            _context.SaveChanges();

            return Ok(Employee);
        }

        [HttpPut]

        public async Task<ActionResult<Employee>> editEmployee(Employee employee)
        {
            //ToDo valiadacia ID filter
            //Todo exception co ked sa vymaze pocas editu?
            var Found_Employee = await _context.Employees.FindAsync(employee.Id);
            if (Found_Employee is null)
            {
                return NotFound("Employee not found");
            }
            Found_Employee.Name = employee.Name;
            Found_Employee.Lastname = employee.Lastname;
            Found_Employee.Title = employee.Title;
            Found_Employee.Id_workplace = employee.Id_workplace;
            _context.SaveChanges();
            return Ok(Found_Employee);
        }
    }
}

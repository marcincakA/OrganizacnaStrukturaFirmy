using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganizacnaStrukturaFirmy.Controllers.Filters.ActionFilters;
using OrganizacnaStrukturaFirmy.Controllers.Filters.ExceptionFilters;
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
        [ServiceFilter(typeof(Employee_ValidateWorkplaceIdExistanceFilterAttribute))]
        public async Task<ActionResult<List<Employee>>> addEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(getEmployeeById), new {id = employee.Id}, employee);
        }

        [HttpGet("{id}")]
        [ServiceFilter(typeof(Employee_ValidateEmployeeIdAttribute))]
        public async Task<ActionResult<Employee>> getEmployeeById(int id)
        {
            var Employee = await _context.Employees.FindAsync(id);
            return Ok(Employee);
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> getEmployees()
        {
            var heroes = await _context.Employees.ToListAsync();
            return Ok(heroes);
        }

        [HttpGet("workplace/{Id_workplace}")]
        public async Task<ActionResult<List<Employee>>> getEmployeesWorkingAt(int Id_workplace)
        {
            var Employee = await _context.Employees.Where(employee => employee.Id_workplace == Id_workplace).ToListAsync();
            return Ok(Employee);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(Employee_ValidateEmployeeIdAttribute))]
        [ServiceFilter(typeof(Employee_ValidateDeleteFilterAttribute))]
        public async Task<ActionResult<Employee>> deleteEmployee(int id)
        {
            var Employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(Employee);
            await _context.SaveChangesAsync();
            return Ok(Employee);
        }

        //if head of node, set node.headId to null and delete employee
        [HttpDelete("force/{id}")]
        [ServiceFilter(typeof(Employee_ValidateEmployeeIdAttribute))]
        public async Task<ActionResult<Employee>> forceDeleteEmployee(int id)
        {
            var node = await _context.Nodes.FirstOrDefaultAsync(n => n.Id_headEmployee == id);
            var employee = await _context.Employees.FindAsync(id);
            if (node == null)
            {
                await deleteEmployee(id);
                return Ok(employee);
            }
            node.Id_headEmployee = null;
            await _context.SaveChangesAsync();
            await deleteEmployee(id);
            return Ok(employee);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(Employee_ValidateEmployeeIdAttribute))]
        [Employee_ValidateEmployeeFilter]
        [ServiceFilter(typeof(Employee_HandleUpdateExceptionsFilterAttribute))]
        [ServiceFilter(typeof(Employee_ValidateWorkplaceIdExistanceFilterAttribute))]

        public async Task<ActionResult<Employee>> editEmployee(int id, Employee employee)
        {
            var Found_Employee = await _context.Employees.FindAsync(employee.Id);
            Found_Employee.Name = employee.Name;
            Found_Employee.Lastname = employee.Lastname;
            Found_Employee.Title = employee.Title;
            Found_Employee.Id_workplace = employee.Id_workplace;
            Found_Employee.Email = employee.Email;
            Found_Employee.Phone = employee.Phone;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("getHeadEmployees/{level}")]
        //filter pozri ci je level 1-4

    }
}

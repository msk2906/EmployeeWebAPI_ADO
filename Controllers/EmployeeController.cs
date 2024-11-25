using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model.Entities;
using WebApplication1.Repository;

namespace WebApplication1.Controllers
{
    //https:localhost7072//api/EmployeeController
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _employeeRepository = new EmployeeRepository(connectionString);
        }

        // GET: api/employee
        [HttpGet]
        public IActionResult GetEmployees()
        {
            var employees = _employeeRepository.GetEmployees();
            return Ok(employees);
        }

        // GET: api/employee/5
        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // POST: api/employee
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            _employeeRepository.AddEmployee(employee);
            return Ok($"Employee with EmpID {employee.EmpID} Added Successfully");
        }

        // PUT: api/employee/5
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            _employeeRepository.UpdateEmployee(id, employee);
            return Ok($"Employee with EmpID {id} Updated Successfully");
        }

        // DELETE: api/employee/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var existingEmployee = _employeeRepository.GetEmployeeById(id);

            _employeeRepository.DeleteEmployee(id);
            return Ok($"Employee with EmpID {id} Deleted Successfully");
        }
    }
}

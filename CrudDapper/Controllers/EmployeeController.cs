using CrudDapper.Model;
using CrudDapper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await _service.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _service.GetEmployeeById(id);
                if (employee == null)
                    return NotFound();
                return Ok(employee);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeById(int id)
        {
            try
            {
                var emp = await _service.GetEmployeeById(id);
                if (emp == null)
                    return NotFound();
                await _service.DeleteEmployee(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateEmp(Employee employee)
        {
            try
            {
                var createdEmployee = await _service.CreateEpluyee(employee);
                return CreatedAtRoute( new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmp(int id, Employee employee)
        {
            try
            {
                var emp = await _service.GetEmployeeById(id);
                if (emp == null)
                    return NotFound();
                await _service.UpdateEpluyee(id, employee);
                return NoContent();
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }
        }
    }
}

using Eshop.DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace PracticeForToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController(IEmployeeServiceContract employeeServiceContract) : ControllerBase
    {
        private readonly IEmployeeServiceContract _employeeServiceContract = employeeServiceContract??throw new ArgumentNullException(nameof(employeeServiceContract));

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            return Ok(await _employeeServiceContract.GetAllAsync());
        }
    }
}

using EmployeeInfoAPI.Data;
using EmployeeInfoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EmployeeInfoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDbContext _employeeDbContext;
        public EmployeeController(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeDb>> Get([FromQuery] CursorParams @params)
        {
            // Filtering the data based on the query param
            var employees = await _employeeDbContext.EmployeeDbs
                .OrderBy(e => e.EmployeeNumber)
                .Where(e => e.EmployeeNumber > @params.Cursor)
                .Take(@params.Count)
                .ToListAsync();

            var nextCursor = employees.Any() 
                ? employees.LastOrDefault()?.EmployeeNumber 
                : 0;

            Response.Headers.Add("X-Pagination", $"Next Cursor={nextCursor}");

            return employees;

        }
    }
}

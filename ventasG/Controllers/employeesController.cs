using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ventasG.Models;

namespace ventasG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class employeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public employeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<employee>>> GetEmployee_TB()
        {
            return await _context.Employee_TB.ToListAsync();
        }

        // GET: api/employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<employee>> Getemployee(int id)
        {
            var employee = await _context.Employee_TB.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putemployee(int id, employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!employeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<employee>> Postemployee(employee employee)
        {
            _context.Employee_TB.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getemployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteemployee(int id)
        {
            var employee = await _context.Employee_TB.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee_TB.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool employeeExists(int id)
        {
            return _context.Employee_TB.Any(e => e.Id == id);
        }
    }
}

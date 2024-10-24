using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ventasG.Models;

namespace ventasG.Controllers
{
    [Authorize]  // Solo los usuarios autenticados pueden acceder
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompany_TB()
        {
            var companies = await _context.Company_TB
         .Select(c => new {
             c.Id,
             c.Name,
             c.description,
             Employees = c.Employees.Select(e => new { e.FullName }),  // Trae solo el nombre de empleados
             Products = c.Products.Select(p => new { p.Name, p.Price }) // Trae solo nombre y precio de productos
         })
         .ToListAsync();

            return Ok(companies);

        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            var company = await _context.Company_TB
             .Include(c => c.Employees)   
             .Include(c => c.Products)   
             .FirstOrDefaultAsync(c => c.Id == id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company); 
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, CreatePutCompanyDto company)
        {
            var companyEdit = await _context.Company_TB.FindAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            // Actualiza solo las propiedades permitidas
            companyEdit.Name = company.Name;
            companyEdit.description = company.Description;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
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

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(CreatePutCompanyDto company)
        {

            var newCompany = new Company { 
            Name = company.Name,
            description = company.Description

            };


            _context.Company_TB.Add(newCompany);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompany", new { id = company.Id }, company);
        }

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var company = await _context.Company_TB.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Company_TB.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompanyExists(int id)
        {
            return _context.Company_TB.Any(e => e.Id == id);
        }
    }
}

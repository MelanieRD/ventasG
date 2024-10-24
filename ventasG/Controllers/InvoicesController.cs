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
    public class InvoicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InvoicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Invoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoice_TB()
        {

            var invoice = await _context.Invoice_TB.Select(
                i => new {
                i.id,
                i.state,
                i.delivery_date,
                i.Orderid
                }
                ).ToListAsync();


            return Ok(invoice);
        }

        // GET: api/Invoices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(int id)
        {
            var invoice = await _context.Invoice_TB.FirstOrDefaultAsync(I => I.id == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        // PUT: api/Invoices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(int id, InvoicePutPostDto invoice)
        {
            var InvoiceEdit = await _context.Invoice_TB.FirstOrDefaultAsync(i => i.id == id);

            if (InvoiceEdit == null) {

                return NotFound();
                
                    }

            InvoiceEdit.state = invoice.state;
            InvoiceEdit.Orderid = invoice.Orderid;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/Invoices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]


        public async Task<ActionResult<Invoice>> PostInvoice(Invoice invoice)
        {

            var date = DateTime.Now;
                var Newinvoice = new Invoice { 
                    id = invoice.id,
                    state = invoice.state,
                    delivery_date = date ,
                   Orderid = invoice.Orderid      
                };


            _context.Invoice_TB.Add(Newinvoice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoice", new { id = invoice.id }, invoice);
        }

        // DELETE: api/Invoices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _context.Invoice_TB.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }

            _context.Invoice_TB.Remove(invoice);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoice_TB.Any(e => e.id == id);
        }
    }
}

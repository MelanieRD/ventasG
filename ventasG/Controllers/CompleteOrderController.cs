using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ventasG.Models;

namespace ventasG.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompleteOrderController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public CompleteOrderController(ApplicationDbContext context)
        {
            _context = context;
        }


        // PUT: api/Orders/CompleteOrder/5
        [HttpPut("CompleteOrder/{id}")]
        public async Task<IActionResult> CompleteOrder(int id)
        {
            var order = await _context.Order_TB.Include(o => o.OrderDetail).FirstOrDefaultAsync(o => o.Orderid == id);
            if (order == null)
            {
                return NotFound();
            }

            // almenos un artículo
            if (order.OrderDetail == null || !order.OrderDetail.Any())
            {
                return BadRequest("No se puede completar la orden sin al menos un artículo asociado.");
            }

            
            order.state = "completada";

            //  factura 
            var date = DateTime.Now;
            var invoice = new Invoice
            {
                Orderid = order.Orderid,
                state = "COMPLETA",
                delivery_date = date
            };

            _context.Invoice_TB.Add(invoice);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
               
                    throw;
                
            }

            return Ok(new { message = "Orden completada y factura generada." });
        }

    }
}

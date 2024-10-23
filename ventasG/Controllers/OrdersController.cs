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
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder_TB()
        {
            return await _context.Order_TB.ToListAsync();

        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var Order = await _context.Order_TB
             .Include(c => c.Employee)
             .Include(c => c.OrderDetail)
             .FirstOrDefaultAsync(c => c.Orderid == id);

            if (Order == null)
            {
                return NotFound();
            }

            return Ok(Order);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderCreatePutDto order)
        {

            var orderEdit = await _context.Order_TB.FindAsync(id);
            if (orderEdit == null) {
               
                return NotFound();

            }

            orderEdit.TotalValue = order.TotalValue;
            orderEdit.state = order.state;
            orderEdit.description = order.description;
            orderEdit.id_OrderDetail = order.id_OrderDetail;
            orderEdit.EmployeeId = orderEdit.EmployeeId;



            

            try
            {
                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Order_TB.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Orderid }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Order_TB.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order_TB.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Order_TB.Any(e => e.Orderid == id);
        }
    }
}

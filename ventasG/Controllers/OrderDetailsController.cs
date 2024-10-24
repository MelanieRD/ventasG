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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/OrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetails>>> GetOrderDetails_TB()
        {
            var orderDetails = await _context.OrderDetails_TB.Select(oD => new {
                oD.id,
                oD.Orderid,
                oD.Productid,
                Product = oD.Product.Name,
                oD.Product.Price


            }).ToListAsync();
            return Ok(orderDetails);
        }

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetails>> GetOrderDetails(int id)
        {
            var orderDetails = await _context.OrderDetails_TB.Select(oD => new {
                oD.id,
                oD.Orderid,
                oD.Productid,
                Product = oD.Product.Name,
                oD.Product.Price


            }).FirstOrDefaultAsync(orderD => orderD.id == id);

            if (orderDetails == null)
            {
                return NotFound();
            }

            return Ok(orderDetails);
        }

        // PUT: api/OrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetails(int id, OrderDetailsPutCreateDto orderDetails)
        {



            var orderDetail = await _context.OrderDetails_TB.FindAsync(id);

            if (id != orderDetails.id )
            {
                return BadRequest();
            }
            orderDetail.Productid = orderDetails.Productid;
            orderDetail.Orderid = orderDetails.Orderid;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailsExists(id))
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

        // POST: api/OrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderDetails>> PostOrderDetails(OrderDetailsPutCreateDto orderD)
        {

            var newOrderDetail = new OrderDetails
            {
             
                id = orderD.id,
                Productid = orderD.Productid,
                Orderid = orderD.Orderid
    };
            _context.OrderDetails_TB.Add(newOrderDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrderDetails", new { id = orderD.id }, orderD);
        }

        // DELETE: api/OrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetails(int id)
        {
            var orderDetails = await _context.OrderDetails_TB.FindAsync(id);
            if (orderDetails == null)
            {
                return NotFound();
            }

            _context.OrderDetails_TB.Remove(orderDetails);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailsExists(int id)
        {
            return _context.OrderDetails_TB.Any(e => e.id == id);
        }
    }
}

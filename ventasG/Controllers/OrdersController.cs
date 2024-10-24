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


            var order = await _context.Order_TB.Select(o => new  {
            o.Orderid,
            o.state,
            o.description,
            o.EmployeeId,
            Employee = o.Employee.FullName,
            o.id_OrderDetail,
            Order_Detail = o.OrderDetail.Select(od => new {od.Product.Name, od.Product.Price }),
            TotalValue = o.OrderDetail.Sum(od => od.Product.Price)


            }).ToListAsync();
            return Ok(order);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var Order = await _context.Order_TB.Select(o => new
            {

                o.Orderid,
                o.state,
                o.description,
                o.EmployeeId,
                Employee = o.Employee.FullName,
                o.id_OrderDetail,
                Order_Detail = o.OrderDetail.Select(od => new { od.Product.Name, od.Product.Price }),
                TotalValue = o.OrderDetail.Sum(od => od.Product.Price)
            }).FirstOrDefaultAsync(c => c.Orderid == id);

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

            if (order.id_OrderDetail == null)
            {
                return BadRequest("La orden debe tener al menos un artículo asociado.");
            }

            var employee = await _context.Employee_TB.Include(e => e.Company).FirstOrDefaultAsync(e => e.Id == order.EmployeeId);

            // Obtener los IDs de los productos asociados a los detalles de la orden
            var productIds = order.OrderDetail.Select(od => od.Productid).ToList();
            var products = await _context.Product_TB.Where(p => productIds.Contains(p.Id)).ToListAsync();

            if (products.Any(p => p.Companyid != employee.Companyid))
            {
                return BadRequest("Todos los artículos deben pertenecer a la misma compañía que el empleado.");
            }

            orderEdit.TotalValue = order.TotalValue;
            orderEdit.state = order.state;
            orderEdit.description = order.description;
            orderEdit.id_OrderDetail = order.id_OrderDetail;
            orderEdit.EmployeeId = orderEdit.EmployeeId; 

            try
            {
               
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
        public async Task<ActionResult<Order>> PostOrder(OrderCreatePutDto order)
        {

            if (order.id_OrderDetail == null)
            {
                return BadRequest("La orden debe tener al menos un artículo asociado.");
            }


            // Verifica que todos los artículos pertenecen a la misma compañía que el empleado
            var employee = await _context.Employee_TB.Include(e => e.Company).FirstOrDefaultAsync(e => e.Id == order.EmployeeId);

            // Obtener los IDs de los productos asociados a los detalles de la orden
            var productIds = order.OrderDetail.Select(od => od.Productid).ToList();
            var products = await _context.Product_TB.Where(p => productIds.Contains(p.Id)).ToListAsync();

           
            if (products.Any(p => p.Companyid != employee.Companyid))
            {
                return BadRequest("Todos los artículos deben pertenecer a la misma compañía que el empleado.");
            }



            var newOrder = new Order
            {

                id_OrderDetail = order.id_OrderDetail,
                EmployeeId = order.EmployeeId,
                state = "pendiente",
                description = order.description,
                TotalValue = 0
  

            };

            _context.Order_TB.Add(newOrder);
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

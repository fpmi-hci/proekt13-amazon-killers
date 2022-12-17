using AmazonKillers.Orders.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using System.Net;

namespace AmazonKillers.Orders.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly OrdersContext _context;

        public OrdersController(
            OrdersContext context)
        {
            _context = context;
        }





        //ORDERS


        [HttpGet]
        [Route("orders/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> OrderByIdAsync(int id)
        {
            var subscription = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);
            return Ok(subscription);
        }

        [HttpGet]
        [Route("orders")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> OrdersAsync()
        {
            var subscription = await _context.Orders
                .Include(o => o.OrderItems)
                .ToListAsync();
            return Ok(subscription);
        }

        [HttpPost]
        [Route("orders")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateOrderAsync(
            [FromBody] Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(OrderByIdAsync), new { id = order.Id }, null);
        }

        [HttpDelete]
        [Route("orders/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteOrderAsync(int id)
        {
            var order = _context.Orders.SingleOrDefault(o => o.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        [Route("subscriptions/user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UserOrdersAsync(string userId)
        {
            var subscriptions = await _context.Orders.Where(o => o.CustomerId == userId)
                .Include(o => o.OrderItems)
                .ToListAsync();
            return Ok(subscriptions);
        }

        [HttpPut]
        [Route("orders")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateOrderStatusAsync([FromBody] Order orderToUpdate)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(a => a.Id == orderToUpdate.Id);
            if (order == null)
            {
                return NotFound(new { Message = $"Order with id {orderToUpdate.Id} not found." });
            }
            if (orderToUpdate.Status != order.Status + 1 
                && !(order.Status == Status.AwaitingDelivery && orderToUpdate.Status == Status.Cancelled))
            {
                return BadRequest(new { Message = $"Can't change order status to {orderToUpdate.Status}." });
            }
            order.Status = orderToUpdate.Status;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(OrderByIdAsync), new { id = orderToUpdate.Id }, null);
        }




        //ADDRESSES



        [HttpGet]
        [Route("addresses/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Address), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddressByIdAsync(int id)
        {
            var subscription = await _context.Addresses
                .FirstOrDefaultAsync(a => a.Id == id);
            return Ok(subscription);
        }

        [HttpPost]
        [Route("addresses")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAddressAsync(
            [FromBody] Address address)
        {
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(OrderByIdAsync), new { id = address.Id }, null);
        }

        [HttpDelete]
        [Route("addresses/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteAddressAsync(int id)
        {
            var address = _context.Addresses.SingleOrDefault(a => a.Id == id);
            if (address == null)
            {
                return NotFound();
            }
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        [Route("addresses")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateAddressAsync([FromBody] Address addressToUpdate)
        {
            var address = await _context.Addresses.SingleOrDefaultAsync(a => a.Id == addressToUpdate.Id);
            if (address == null)
            {
                return NotFound(new { Message = $"Address with id {addressToUpdate.Id} not found." });
            }
            address = addressToUpdate;
            _context.Addresses.Update(address);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(AddressByIdAsync), new { id = addressToUpdate.Id }, null);
        }

        [HttpGet]
        [Route("addresses/user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UserAddressesAsync(string userId)
        {
            var subscriptions = await _context.Addresses.Where(a => a.UserId == userId)
                .ToListAsync();
            return Ok(subscriptions);
        }
    }
}
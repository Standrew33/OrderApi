using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data;
using OrderApi.DTOs;
using OrderApi.Models;
using OrderApi.Services;

namespace OrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _db;
        private readonly PaymentQueueService _paymentQueue;

        public OrderController(OrderDbContext db, PaymentQueueService queue)
        {
            _db = db;
            _paymentQueue = queue;
        }

        [HttpPost]
        public async Task<ActionResult> CreateOrder(OrderCreateDto dto)
        {
            var order = new Order
            {
                OrderNumber = dto.OrderNumber,
                CustomerName = dto.CustomerName,    
                Items = dto.Items.Select(s => new OrderItem
                {
                    ItemName = s.ItemName,
                    Quantity = s.Quantity,
                    UnitPrice = s.UnitPrice
                }).ToList()
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrders), new { id = order.Id });
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            var orders = await _db.Orders.Include(i => i.Items).ToListAsync();
            return Ok(orders);
        }

        [HttpPost("payment")]
        public IActionResult SubmitPayment(PaymentRequestDto payment)
        {
            _paymentQueue.Enqueue(payment);
            return Accepted("Payment received, processing async.");
        }
    }
}

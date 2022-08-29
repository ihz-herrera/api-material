using APIRest.Contextos;
using APIRest.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Controllers
{
    [Route("api/v1/sales")]
    public class SalesController : ControllerBase
    {


        private readonly Context _context;
        private readonly ILogger<SalesController> _logger;

        public SalesController(Context context, ILogger<SalesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("orders")]
        public async Task<IActionResult> QueryOrders()
        {
            var result = await _context.Orders.ToListAsync();

            return Ok(result);

        }

        [HttpGet("orders/details")]
        public async Task<IActionResult> QueryOrdersDetail()
        {
            var result = await _context.Orders
                .Include(o=>o.OrderItems)
                .ThenInclude(o=>o.Item).ToListAsync();

            return Ok(result);

        }

        [HttpPost("orders")]
        public async Task<IActionResult> CreateOrders([FromBody] Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return Ok(order);

        }

    }
}

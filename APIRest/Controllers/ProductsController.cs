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
    [Route("api/v1")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;
        private readonly Context _context;

        public ProductsController(ILogger<ProductsController> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("products")]
        public async Task<IActionResult> AllProduct()
        {

            try
            {
                var result = await _context.Products.ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Error Interno del Servidor");
            }
        }


        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            try
            {
                await _context.AddAsync<Product>(product);
                await _context.SaveChangesAsync();

                return Ok(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Error Interno del Servidor");
            }
        }


    }
}

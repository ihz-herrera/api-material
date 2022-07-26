using APIRest.Contextos;
using APIRest.Entidades;
using APIRest.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Controllers
{
    [Route("api/")]
    public class CustomersController : ControllerBase
    {
        private Context _context;
        private readonly ILogger<CustomersController> _logger;
        

        public CustomersController(Context context,ILogger<CustomersController> logger)
        {
            _context = context;
            _logger = logger;
            
        }

        
        [HttpPost("v1/customer")]
        
        public IActionResult SaveCustomer([FromBody]Customer  customer)
        {
            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();

                _logger.LogInformation("Cliente creado con éxito {id}", customer.CustomerId);

                return Ok();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex.Message);

                return StatusCode(500,new ErrorResponse(){ NoError = 100, 
                    Message= "Error Interno del Servidor"});
            }

            try
            {

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500,  "Error Interno del Servidor");
            }
            
        }



        [HttpGet("v2/customer/{id}")]
        public IActionResult QueryCustomerById([FromRoute]int id)
        {

            try
            {
                var customersList = _context.Customers.Where(c => c.CustomerId == id).ToList();
                return Ok(customersList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Error Interno del Servidor");
            }
        }


        [HttpGet("v1/customer")]
        
        public IActionResult QueryCustomer()
        {

            try
            {
                var customersList = _context.Customers.ToList();
                return Ok(customersList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Error Interno del Servidor");
            }
        }

        [HttpGet("v2/customers")]
        [Authorize]
        public IActionResult QueryCustomer_v2()
        {

            try
            {

                var customersList = _context.Customers.ToList();
                return Ok(customersList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Error Interno del Servidor");
            }
        }


    }
}

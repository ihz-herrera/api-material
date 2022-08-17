using APIRest.Contextos;
using APIRest.Entidades;
using APIRest.Helper;
using FluentValidation;
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
    [ApiController]

    public class CustomersController : ControllerBase
    {
        private readonly Context _context;
        private readonly ILogger<CustomersController> _logger;
        //private readonly IValidator<Customer> _validator;

        public CustomersController(Context context,ILogger<CustomersController> logger
            )
        {
            _context = context;
            _logger = logger;
            //_validator = validator;
            
        }

        
        [HttpPost("v1/customer")]
        public IActionResult SaveCustomer([FromBody]Customer  customer)
        {
            try
            {

                //var validationResult =  _validator.Validate(customer);
                
                //if(!validationResult.IsValid)
                //{
                //    return BadRequest(validationResult.Errors);
                //}


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
        [Authorize(AuthenticationSchemes = "BasicAuthentication")]
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

        [HttpGet("v3/customers")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public IActionResult QueryCustomer_v3()
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

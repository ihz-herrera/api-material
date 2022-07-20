using APIRest.Contextos;
using APIRest.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Controllers
{
    [Route("api/customer")]
    public class CustomersController : ControllerBase
    {
        private Context _context;
        private readonly ILogger<CustomersController> _logger;
        

        public CustomersController(Context context,ILogger<CustomersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        
        [HttpPost]
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

                return StatusCode(500,new { noRastreo = 100, 
                    data= new { mensaje= "Error Interno del Servidor",customer 
                    } });
            }
            
        }



        [HttpGet("{id}")]
        public IActionResult QueryCustomerById([FromRoute]int id)
        {
            var customersList=_context.Customers.Where(c=>c.CustomerId==id).ToList();
            return Ok(customersList);
        }


        [HttpGet]
        public IActionResult QueryCustomer()
        {
            var customersList = _context.Customers.ToList();
            return Ok(customersList);
        }


    }
}

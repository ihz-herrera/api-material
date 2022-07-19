using APIRest.Contextos;
using APIRest.Entidades;
using Microsoft.AspNetCore.Mvc;
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

        public CustomersController(Context context)
        {
            _context = context;
        }

        
        [HttpPost]
        public IActionResult SaveCustomer([FromBody]Customer  customer)
        {
            //Todo: Control de errores
            _context.Customers.Add(customer);
            _context.SaveChanges();

            return Ok();
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

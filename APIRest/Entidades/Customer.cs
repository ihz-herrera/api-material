using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Entidades
{
    public class Customer
    {
        
        public int CustomerId { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}

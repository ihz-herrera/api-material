using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Entidades
{
    public class Customer
    {
        
        //[Required]
        public int CustomerId { get; set; }
        //[Required]
        //[MaxLength(10)]
        public string Description { get; set; }
        public bool Status { get; set; }
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }
    }
}

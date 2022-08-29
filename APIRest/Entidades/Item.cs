using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Entidades
{
    [Table("products",Schema="production")]
    public class Item
    {
        [Key]
        public int product_id { get; set; }
        public string product_name { get; set; }
    }
}

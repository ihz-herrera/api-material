using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Entidades
{
    [Table("order_items",Schema="sales")]
    public class OrderItem
    {

        [Key]
        public int line_id { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }
        public int item_id { get; set; }
        
        public int quantity { get; set; }
        public decimal list_price { get; set; }
        public decimal discount { get; set; }

        [ForeignKey("Item")]
        public int product_id { get; set; }
        public Item Item { get; set; }

    }
}

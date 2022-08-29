using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Entidades
{
	[Table("Orders",Schema ="sales")]
    public class Order
    {
		[Key]
		[Column("order_id")]
		public int OrderId { get; set; }

		public int? customer_id { get; set; }
		public byte order_status { get; set; }
		public DateTime order_date { get; set; }
		public DateTime required_date { get; set; }
		public DateTime? shipped_date { get; set; }
		public int store_id { get; set; }
		public int staff_id { get; set; }

		//Propiedad de Navegacion
        public List<OrderItem> OrderItems { get; set; }
    }
}


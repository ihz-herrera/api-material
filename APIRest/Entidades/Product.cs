using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Entidades
{
	[Table("genRegistroProductosDesdeElPuntodeVentaCat")]
    public class Product
    {
		[Key]
		[Column("CodigoProducto")]
		public int ProductId { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
		public decimal Cost { get; set; }
		public bool Status { get; set; }
		public DateTime CreateAt { get; set; }
	}
}


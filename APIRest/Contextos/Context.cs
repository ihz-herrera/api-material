using APIRest.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIRest.Contextos
{
    public class Context:DbContext
    {

        public DbSet<Customer> Customers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Todo: Recibir cadena de coneccion por parametro
                optionsBuilder.UseSqlServer(@"Server=LAP-IVANH\MSSQLSERVER01; Initial Catalog=BDEntrenamiento; Trusted_Connection=True");
            }
        }
    }
}

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


        public Context(DbContextOptions options):base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        
        //        optionsBuilder.UseSqlServer(@"Server=LAP-IVANH\MSSQLSERVER01; Initial Catalog=BDEntrenamiento; Trusted_Connection=True");
        //    }
        //}
    }
}

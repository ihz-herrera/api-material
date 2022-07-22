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

        
    }
}

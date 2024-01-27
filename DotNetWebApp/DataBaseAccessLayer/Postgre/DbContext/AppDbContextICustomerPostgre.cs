using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataBaseAccessLayer.Postgre.DbContext
{
    public class AppDbContextICustomerPostgre : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<InternalCustomer> Icustomers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5432;Database=DotNetWebApp;Username=postgres;Password=root;");
        }
    }
}

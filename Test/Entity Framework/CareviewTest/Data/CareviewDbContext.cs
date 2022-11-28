using CareviewTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareviewTest.Data
{
    public class CareviewDbContext : DbContext
    {
        public CareviewDbContext():base("careViewEntities")
        {
            Database.SetInitializer(new DatabaseInitializer());
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>().Property(c => c.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Client>().Property(c => c.DateOfBirth).IsRequired().HasColumnType("datetime2");
            modelBuilder.Entity<Client>().Property(c => c.NDISNumber).HasMaxLength(20) ;
            modelBuilder.Entity<Invoice>().Property(c => c.InvoiceNumber).HasMaxLength(50) ;
        }
    }
}

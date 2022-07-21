using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EFC_Final.Models;
using Microsoft.EntityFrameworkCore;
namespace EFC_Final
{
    internal class TourAppDbContext : DbContext
    {
        
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Guide> Guides { get; set; }
        public DbSet<Visits> ToVisits { get; set; }
        public DbSet<CustomerVisits> CustomerVisits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=TourAppD;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }











        
        
    }
}

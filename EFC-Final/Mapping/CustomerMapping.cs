using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFC_Final.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFC_Final.Mapping
{
    internal class CustomerMapping : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.CustomerId);
            builder.HasKey(c => new { c.ToVisits, c.Countries, c.Guides, c.Genders });
            
            builder.Property(v => v.CustomerId).HasColumnType("char");
            builder.Property(c => c.ToVisits).HasColumnType("char");
            builder.Property(c => c.Countries).HasColumnType("char");
            builder.Property(c => c.Guides).HasColumnType("char");
            builder.Property(c => c.Genders).HasColumnType("char");
            
            builder.Property(c => c.FirstName).HasColumnType("String").HasMaxLength(20);
            builder.Property(c => c.LastName).HasColumnType("String").HasMaxLength(40);
            builder.Property(c => c.ArrivingDate).HasColumnType("DateTime");
            builder.Property(c => c.Bday).HasColumnType("DateTime");

            builder.HasOne(a => a.Genders)
                .WithMany(a => a.Customers);
            
            builder.HasOne(a => a.Countries)
                  .WithMany(a => a.Customers);

            builder.HasOne(a => a.Guides)
               .WithMany(a => a.Customers);

            builder.HasMany(a => a.ToVisits)
               .WithMany(a => a.Customers);

          


        }
    }
       
    

    


    
}

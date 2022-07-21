using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EFC_Final.Models;

namespace EFC_Final.Mapping
{
    internal class CountryMapping : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {

            builder.HasKey(g => g.CountryId);
            builder.HasKey(c => new { c.Customers});
            builder.Property(v => v.CountryId).HasColumnType("char");
            builder.Property(c => c.Customers).HasColumnType("char");
            builder.Property(c => c.CName).HasColumnType("String");
           


            builder.HasMany(a => a.Customers)
                  .WithOne(a => a.Countries);









          
        }
    }
    
    
}

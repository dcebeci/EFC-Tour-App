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
    internal class GenderMappping : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasKey(g => g.GenderId);
            builder.HasKey(g => new { g.Customers });
            builder.Property(v => v.GenderId).HasColumnType("char");
            builder.Property(g => g.Customers).HasColumnType("char");
            builder.Property(g => g.GenderType).HasColumnType("String");
            
            builder.HasMany(a => a.Customers)
                   .WithOne(a => a.Genders);



          
        }
    }
}

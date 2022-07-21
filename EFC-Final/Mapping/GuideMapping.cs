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
    internal class GuideMapping : IEntityTypeConfiguration<Guide>
    {
        public void Configure(EntityTypeBuilder<Guide> builder)
        {
            builder.HasKey(g => g.GuideId);
            builder.HasKey(g => new { g.Customers });
            builder.Property(v => v.GuideId).HasColumnType("char");
            builder.Property(g => g.FirstName).HasColumnType("String").HasMaxLength(20);
            builder.Property(g => g.LastName).HasColumnType("String").HasMaxLength(40);
            builder.Property(g => g.PhoneNumber).HasColumnType("Int");
            


            builder.HasMany(a => a.Customers)
                   .WithOne(a => a.Guides);

     

        }
    }
    
    
    }


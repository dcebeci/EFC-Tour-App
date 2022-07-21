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
    internal class VisitsMapping : IEntityTypeConfiguration<Visits>
    {
        public void Configure(EntityTypeBuilder<Visits> builder)
        {
            builder.HasKey(v => v.VisitId);
            builder.HasKey(v => new {v.Customers});
            builder.Property(v => v.VisitId).HasColumnType("char");
            builder.Property(v => v.Customers).HasColumnType("char");
            builder.Property(v => v.LocationName).HasColumnType("String").HasMaxLength(30);

            builder.HasMany(a => a.Customers)
                   .WithMany(a => a.ToVisits);

         


        }
    }
}

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
    internal class CustomerVisitsMapping : IEntityTypeConfiguration<CustomerVisits>
    {
        public void Configure(EntityTypeBuilder<CustomerVisits> builder)
        {



            builder.HasKey(c => new { c.CustomerId, c.VisitId });
            
            builder.HasOne(c => c.Customer)
                .WithMany(a => a.CustomerVisit)
                .HasForeignKey(c => c.CustomerId);


            builder.HasOne(a => a.ToVisits)
              .WithMany(a => a.CustomerVisit)
              .HasForeignKey(a=> a.VisitId);







        }
    }
    
    
}

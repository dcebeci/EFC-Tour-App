using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using EFC_Final.Mapping;

namespace EFC_Final.Models
{

    internal class CustomerVisits
    {
        public int CustomerId { get; set; }
        public int VisitId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("VisitId")]
        public Visits ToVisits { get; set; }
    }
}

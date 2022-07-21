using EFC_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFC_Final.Models
{

    internal class Visits 
    {
        public Visits()
        {
            Customers = new HashSet<Customer>();
            CustomerVisit = new HashSet<CustomerVisits>();
        }
        public string LocationName { get; set; }
        public int VisitId { get; set; }
        public ICollection<Customer> Customers { get; set; }
        public ICollection<CustomerVisits> CustomerVisit { get; set; }

    }
}

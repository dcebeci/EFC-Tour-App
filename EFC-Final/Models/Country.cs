using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFC_Final.Models
{
    internal class Country
    {
        public Country()
        {
            Customers = new HashSet<Customer>();
        }
        public int CountryId { get; set; }
        public string CName { get; set; }
        public ICollection<Customer> Customers { get; set;}
    }
}

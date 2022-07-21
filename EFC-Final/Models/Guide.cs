using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFC_Final.Models
{
    internal class Guide
    {
        public Guide()
        {
            Customers = new HashSet<Customer>();    
        }
        public int GuideId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }    
        public ICollection<Customer> Customers { get; set; }
    }
}

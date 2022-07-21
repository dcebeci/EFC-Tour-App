using EFC_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFC_Final.Models
{
    internal class Customer
    {
        public Customer()
        {
            ToVisits = new HashSet<Visits>();
            CustomerVisit = new HashSet<CustomerVisits>();
        }


        public int CustomerId { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public Gender Genders { get; set; } 
        public DateTime Bday { get; set; }
        public Country Citizenship { get; set; }
        public Country Countries { get; set; }
        public Guide Guides { get; set; }
        public DateTime ArrivingDate { get; set; }  
        public ICollection<Visits> ToVisits { get; set; }
        public ICollection<CustomerVisits> CustomerVisit { get; set; }



    }
}

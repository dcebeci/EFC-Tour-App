using EFC_Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFC_Final.Models
{
    internal class Gender
    {
        public int GenderId { get; set; }
        public string GenderType { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}

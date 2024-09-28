using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    public class Employee:Person
    {
        public string Contract { get; set; }
        public string Depatment { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.Dto
{
    public class EmployeeDto
    {
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportSeriya { get; set; }
        public string PassportNumber { get; set; }
        public string Contract { get; set; }
        public string Depatment { get; set; }
        public string JobTitle { get; set; }
        public int Salary { get; set; }
    }
}

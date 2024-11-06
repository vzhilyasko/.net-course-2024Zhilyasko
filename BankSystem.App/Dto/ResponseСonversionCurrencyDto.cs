using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.Dto
{
    public class ResponseСonversionCurrencyDto
    {
        public int Error { get; set; }
        public string ErrorMessage { get; set; }
        public double Amount { get; set; }
    }
}

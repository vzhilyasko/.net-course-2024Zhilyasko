using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.App.Exceptions
{
    internal class PassportException: Exception
    {
        public PassportException(string message)
            : base(message) { }
    }
}

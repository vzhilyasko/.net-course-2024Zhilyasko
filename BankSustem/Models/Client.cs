using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    //Клиент
    internal class Client:Person
    {
        public Client(string _firstName, string _lastName, string _midlleName, DateTime _birthday, string _email, string _phoneNumber) : base(_firstName, _lastName, _midlleName, _birthday, _email, _phoneNumber)
        {
        }
    }
}

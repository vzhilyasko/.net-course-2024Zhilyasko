using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Models
{
    //Валюта
    public struct Currency
    {
        public string Code; 
        public string Name;
        public double ExchangeRate;

        public void UpdateCurrency(Currency currency)
        {
            Code = currency.Code;
            Name = currency.Name;
            ExchangeRate = currency.ExchangeRate;
        }
        
        public string GetInfoCurrency()
        {
            return "Код: " + this.Code + "; наименование: " +this.Name + "; Курс: " + this.ExchangeRate;
        }
    }


}

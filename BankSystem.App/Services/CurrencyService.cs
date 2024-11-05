using BankSystem.App.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Models;
using Newtonsoft.Json;

namespace BankSystem.App.Services
{
    public class CurrencyService
    {
        private readonly string _apiKey;
        private readonly string _url;

        public CurrencyService(string apiKey, string url)
        {
            _apiKey = apiKey;
            _url = url;
        }

        public async Task<CurrencyDto> GetCurrency(Currency data)
        {
            UriBuilder builder = new UriBuilder(_url);

            var query = System.Web.HttpUtility.ParseQueryString(builder.Query);
            query["api_key"] = _apiKey;
            query["from"] = data.From;
            query["to"] = data.To;
            query["amount"] = data.Amount.ToString();

            builder.Query = query.ToString();

            string finalUrl = builder.ToString();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(finalUrl);
                string message = await responseMessage.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<CurrencyDto>(message);
            }
        }
    }
}

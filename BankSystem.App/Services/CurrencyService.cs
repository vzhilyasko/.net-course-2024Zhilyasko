using BankSystem.App.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Models;
using Newtonsoft.Json;
using System.Threading;
using BankSystem.App.Exceptions;

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

        public async Task<ResponseСonversionCurrencyDto> GetCurrencyСonversion(CurrencyConversion dataСurrency, CancellationToken cancellationToken)
        {
            UriBuilder builder = new UriBuilder(_url);

            var query = System.Web.HttpUtility.ParseQueryString(builder.Query);
            query["api_key"] = _apiKey;
            query["from"] = dataСurrency.From;
            query["to"] = dataСurrency.To;
            query["amount"] = dataСurrency.Amount.ToString();

            builder.Query = query.ToString();

            string finalUrl = builder.ToString();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage responseMessage = await client.GetAsync(finalUrl);

                if (responseMessage.StatusCode != HttpStatusCode.OK)
                {
                    throw new CurrencyConversionException(responseMessage.StatusCode.ToString());
                }
                
                string message = await responseMessage.Content.ReadAsStringAsync(cancellationToken);
                
                return JsonConvert.DeserializeObject<ResponseСonversionCurrencyDto>(message);
            }
        }
    }
}

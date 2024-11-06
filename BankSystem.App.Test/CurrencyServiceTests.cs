using BankSystem.App.Dto;
using BankSystem.App.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSystem.Models;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace BankSystem.App.Tests
{
    public class CurrencyServiceTests
    {
        CurrencyService _currencyConversionService;
        CancellationToken _cancellationToken;

        public CurrencyServiceTests()
        {
           _currencyConversionService = new CurrencyService(BankSystem.App.Tests.Properties.Resources.apiKey, BankSystem.App.Tests.Properties.Resources.uri);
        }

        [Fact]
        public async Task GetCurrencyConversion()
        {
            CurrencyConversion currencyConversion = new CurrencyConversion()
            {
                To = "USD",
                From = "EUR",
                Amount = 1000
            };
            ResponseСonversionCurrencyDto currencyResponse = await _currencyConversionService.GetCurrencyСonversion(currencyConversion, _cancellationToken);
            Assert.NotNull(currencyResponse);
        }
    }
}

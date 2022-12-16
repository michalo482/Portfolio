using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.FinancialModelingPrepAPI.Results;
using TraderOop.Domain.Exceptions;
using TraderOop.Domain.Services;

namespace Trader.FinancialModelingPrepAPI.Services
{
    public class CurrencyPriceService : ICurrencyPriceService
    {
        public async Task<decimal> GetPrice(string symbol)
        {
            string uri = $"{symbol}/?format=json";

            using (FinancialModelingPrepHttpClient client = new FinancialModelingPrepHttpClient())
            {
                CurrencyPriceResult currencyPriceResult = await client.GetAsync<CurrencyPriceResult>(uri);

                if (currencyPriceResult.Mid == 0)
                {
                    throw new InvalidSymbolException(symbol);
                }

                return currencyPriceResult.Mid;
            }
        }
    }
}

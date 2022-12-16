using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderOop.Domain.Models;
using TraderOop.Domain.Services;

namespace Trader.FinancialModelingPrepAPI.Services
{
    public class CurrencyService : ICurrencyService
    {
        public async Task<CurrencyModel> GetCurrency(CurrencyCode currencyCode)
        {
            string uri = $"{GetUriSuffix(currencyCode)}/?format=json";

            using (FinancialModelingPrepHttpClient client = new FinancialModelingPrepHttpClient())
            {
                CurrencyModel currencyModel = await client.GetAsync<CurrencyModel>(uri);
                return currencyModel;
            }
        }

        private string GetUriSuffix(CurrencyCode currencyCode)
        {
            switch(currencyCode)
            {
                case CurrencyCode.USD:
                    return "USD";
                case CurrencyCode.CHF:
                    return "CHF";
                case CurrencyCode.GBP:
                    return "GBP";
                default:
                    throw new Exception("Currency code not supported");
            }
        }
    }
}

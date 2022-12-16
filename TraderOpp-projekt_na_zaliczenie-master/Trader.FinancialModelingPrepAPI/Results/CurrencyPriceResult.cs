using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderOop.Domain.Models;

namespace Trader.FinancialModelingPrepAPI.Results
{
    public class CurrencyPriceResult
    {
        public List<Rate> Rates { get; set; }

        public decimal Mid
        {
            get
            {
                return Rates.First().Mid;
            }
        }
    }
}

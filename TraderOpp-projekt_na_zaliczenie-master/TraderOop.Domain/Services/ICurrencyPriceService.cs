using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TraderOop.Domain.Exceptions;
using System.Threading.Tasks;

namespace TraderOop.Domain.Services
{
    public interface ICurrencyPriceService
    {
        /// <summary>
        /// Pobiera cene waluty zapisanej jako symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns>cene waluty o podanym symbolu</returns>
        /// <exception cref="InvalidSymbolException">jesli symbol nie istnieje</exception>
        /// <exception cref="Exception">jeżeli pobranie waluty o symbolu nie powiedzie się</exception>
        Task<decimal> GetPrice(string symbol);
    }
}

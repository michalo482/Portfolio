using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderOop.Domain.Models;
using TraderOop.Domain.Exceptions;

namespace TraderOop.Domain.Services.TransactionServices
{
    public interface ISellService
    {
        /// <summary>
        /// Sprzedaż walut dla danego konta
        /// </summary>
        /// <param name="account"></param>
        /// <param name="symbol"></param>
        /// <param name="shares"></param>
        /// <returns>zaktualizowane konto</returns>
        /// <exception cref="InsufficientSharesException">w przypadku mniejszej ilości waluty w portfelu niż ilości do sprzedania</exception> 
        /// <exception cref="InvalidSymbolException">w przypadku błędnego symbolu kupowanej waluty</exception>
        /// <exception cref="Exception">jeśli tranzakcja sie nie powiedzie</exception>
        Task<Account> Sell(Account account, string symbol, int shares);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderOop.Domain.Models;
using TraderOop.Domain.Exceptions;

namespace TraderOop.Domain.Services.TransactionServices
{
    public interface IBuyService
    {
        /// <summary>
        /// Kupuje walute dla aktualnego (konta)użytkownika.
        /// </summary>
        /// <param name="buyer"></param>
        /// <param name="symbol"></param>
        /// <param name="amount"></param>
        /// <returns>Zauktualizowany stan (konta)użytkownika</returns>
        /// <exception cref="InsufficientFundsException">W przypadku braku środków</exception>
        /// <exception cref="InvalidSymbolException">w przypadku błędnego symbolu kupowanej waluty</exception>
        /// <exception cref="Exception">jeśli tranzakcja sie nie powiedzie</exception>
        Task<Account> BuyCurrency(Account buyer, string symbol, int amount);
    }
}

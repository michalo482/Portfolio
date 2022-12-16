using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderOop.Domain.Exceptions;
using TraderOop.Domain.Models;

namespace TraderOop.Domain.Services.TransactionServices
{
    public class SellService : ISellService
    {

        private readonly ICurrencyPriceService _currencyPriceService;
        private readonly IDataService<Account> _dataService;

        public SellService(ICurrencyPriceService currencyPriceService, IDataService<Account> dataService)
        {
            _currencyPriceService = currencyPriceService;
            _dataService = dataService;
        }

        public async Task<Account> Sell(Account account, string symbol, int shares)
        {
            // walidacja sprzedającego i ilości posiadanych walut
            int accountShares = GetAccountSharesBySymbol(account, symbol);
            if (accountShares < shares)
            {
                throw new InsufficientSharesException(symbol, accountShares, shares);
            }
            //Get z ceną waluty o podanym symbolu
            decimal currencyPrice = await _currencyPriceService.GetPrice(symbol);
            //dodać AssetTransaction do konta ze sprzedażą
            account.AssetTransactions.Add(new AssetTransaction()
                {
                Account = account,
                Stock = new Stock()
                {
                    Symbol = symbol,
                    Price = currencyPrice
                },
                DateProcessed = DateTime.Now,
                Shares = shares,
                IsPurchase = false
            }
            );
            //zwrócić zaktualizowaną kopię konta
            account.Balance += currencyPrice * shares;

            await _dataService.Update(account.Id, account);

            return account;
        }

        private int GetAccountSharesBySymbol(Account account, string symbol)
        {
            IEnumerable<AssetTransaction> accountTransactionsForSymbol = account.AssetTransactions.Where(x => x.Stock.Symbol == symbol);
            return accountTransactionsForSymbol.Sum(x => x.IsPurchase ? x.Shares : -x.Shares);

        }
    }
}

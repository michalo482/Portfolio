using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderOop.Domain.Exceptions;
using TraderOop.Domain.Models;

namespace TraderOop.Domain.Services.TransactionServices
{
    public class BuyService : IBuyService
    {
        private readonly ICurrencyPriceService _currencyPriceService;
        private readonly IDataService<Account> _accountService;

        public BuyService(ICurrencyPriceService currencyPriceService, IDataService<Account> accountService)
        {
            _currencyPriceService = currencyPriceService;
            _accountService = accountService;
        }

        public async Task<Account> BuyCurrency(Account buyer, string symbol, int amount)
        {
            decimal currencyPrice = await _currencyPriceService.GetPrice(symbol);

            decimal transactionPrice = currencyPrice * amount;


            if (transactionPrice > buyer.Balance)
            {
                throw new InsufficientFundsException(buyer.Balance, transactionPrice);
            }

            AssetTransaction transaction = new AssetTransaction()
            {
                Account = buyer,
                Stock = new Stock()
                {
                    Symbol = symbol,
                    Price = currencyPrice
                },
                DateProcessed = DateTime.Now,
                Shares = amount,
                IsPurchase = true
            };

            buyer.AssetTransactions.Add(transaction);
            buyer.Balance -= transactionPrice;

            await _accountService.Update(buyer.Id, buyer);

            return buyer;
        }
    }
}

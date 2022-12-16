using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderOop.Domain.Exceptions;
using TraderOop.Domain.Models;
using TraderOop.Domain.Services;
using TraderOop.Domain.Services.TransactionServices;

namespace Trader.Domain.Tests.Services.TransactionServices
{
    [TestFixture]
    public class SellServiceTests
    {
        private SellService _sellService;

        private Mock<ICurrencyPriceService> _mockCurrencyPriceService;
        private Mock<IDataService<Account>> _mockDataService;
        
        [SetUp]
        public void SetUp()
        {
            _mockCurrencyPriceService = new Mock<ICurrencyPriceService>();
            _mockDataService = new Mock<IDataService<Account>>();
            
            _sellService = new SellService(_mockCurrencyPriceService.Object, _mockDataService.Object);
        }

        [Test]
        public void SellCurrency_WithInsufficientShares_ThrowsInsufficientSharesException()
        {
            string expectedSymbol = "USD";
            int expectedAccountShares = 0;
            int expectedRequiredShares = 10;
            Account seller = CreateAccount(expectedSymbol, expectedAccountShares);

            InsufficientSharesException exception = Assert.ThrowsAsync<InsufficientSharesException>(
                () => _sellService.Sell(seller, expectedSymbol, expectedRequiredShares));
            string actualSymbol = exception.Symbol;
            double actualAccountBalance = exception.AvailableShares;
            double actualRequiredBalance = exception.RequiredShares;

            Assert.AreEqual(expectedSymbol, actualSymbol);
            Assert.AreEqual(expectedAccountShares, actualAccountBalance);
            Assert.AreEqual(expectedRequiredShares, actualRequiredBalance);

        }

        private static Account CreateAccount(string symbol, int shares)
        {
            return new Account()
            {
                AssetTransactions = new List<AssetTransaction>()
                {
                    new AssetTransaction()
                    {
                        Stock = new Stock()
                        {
                            Symbol = symbol
                        },
                        IsPurchase = true,
                        Shares = shares
                    }
                }
            };
        }

        [Test]
        public void SellCurrency_WithInvalidSymbol_ThrowsInvalidSymbolException()
        {
            string expectedInvalidSymbol = "bad_symbol";
            Account seller = CreateAccount(expectedInvalidSymbol, 10);
            _mockCurrencyPriceService.Setup(s => s.GetPrice(expectedInvalidSymbol)).ThrowsAsync(new InvalidSymbolException(expectedInvalidSymbol));

            InvalidSymbolException exception = Assert.ThrowsAsync<InvalidSymbolException>(() => _sellService.Sell(seller, expectedInvalidSymbol, 5));
            string actualInvalidSymbol = exception.Code;

            Assert.AreEqual(expectedInvalidSymbol, actualInvalidSymbol);

        }

        [Test]
        public void SellStock_WithGetPriceFailure_ThrowsException()
        {
            Account seller = CreateAccount(It.IsAny<string>(), 10);
            _mockCurrencyPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(() => _sellService.Sell(seller, It.IsAny<string>(), 5));
        }

        [Test]
        public void SellStock_WithAccountUpdateFailure_ThrowsException()
        {
            Account seller = CreateAccount(It.IsAny<string>(), 10);
            _mockDataService.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<Account>())).ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(() => _sellService.Sell(seller, It.IsAny<string>(), 5));
        }

        [Test]
        public async Task SellStock_WithSuccessfulSell_ReturnsAccountWithNewTransaction()
        {
            int expectedTransactionCount = 2;
            Account seller = CreateAccount(It.IsAny<string>(), 10);

            seller = await _sellService.Sell(seller, It.IsAny<string>(), 5);
            int actualTransactionCount = seller.AssetTransactions.Count;

            Assert.AreEqual(expectedTransactionCount, actualTransactionCount);
        }

        [Test]
        public async Task SellStock_WithSuccessfulSell_ReturnsAccountWithNewBalance()
        {
            decimal expectedBalance = -100;
            Account seller = CreateAccount(It.IsAny<string>(), 10);
            _mockCurrencyPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ReturnsAsync(50);

            seller = await _sellService.Sell(seller, It.IsAny<string>(), 2);
            decimal actualBalance = seller.Balance;

            Assert.AreEqual(expectedBalance, actualBalance);
        }



    }
}

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
    public class BuyServiceTests
    {

        
        private Mock<ICurrencyPriceService> _mockCurrencyPriceService;
        private Mock<IDataService<Account>> _mockAccountService;
        private BuyService _buySService;

        [SetUp]
        public void SetUp()
        {
            _mockCurrencyPriceService = new Mock<ICurrencyPriceService>();
            _mockAccountService = new Mock<IDataService<Account>>();

            _buySService = new BuyService(_mockCurrencyPriceService.Object, _mockAccountService.Object);
        }

        [Test]
        public void BuyStock_WithInvalidSymbol_ThrowsInvalidSymbolExceptionForSymbol()
        {
            string expectedInvalidSymbol = "bad_symbol";
            _mockCurrencyPriceService.Setup(s => s.GetPrice(expectedInvalidSymbol)).ThrowsAsync(new InvalidSymbolException(expectedInvalidSymbol));

            InvalidSymbolException excpetion = Assert.ThrowsAsync<InvalidSymbolException>(
                () => _buySService.BuyCurrency(It.IsAny<Account>(), expectedInvalidSymbol, It.IsAny<int>()));
            string actualInvalidSymbol = excpetion.Code;

            Assert.AreEqual(expectedInvalidSymbol, actualInvalidSymbol);
        }

        [Test]
        public void BuyStock_WithGetPriceFailure_ThrowsException()
        {
            _mockCurrencyPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(
                () => _buySService.BuyCurrency(It.IsAny<Account>(), It.IsAny<string>(), It.IsAny<int>()));
        }

        [Test]
        public void BuyStock_WithInsufficientFunds_ThrowsInsufficientFundsExceptionForBalances()
        {
            decimal expectedAccountBalance = 0;
            decimal expectedRequiredBalance = 100;
            Account buyer = CreateAccount(expectedAccountBalance);
            _mockCurrencyPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ReturnsAsync(expectedRequiredBalance);

            InsufficientFundsException exception = Assert.ThrowsAsync<InsufficientFundsException>(
                () => _buySService.BuyCurrency(buyer, It.IsAny<string>(), 1));
            decimal actualAccountBalance = exception.AccountBalance;
            decimal actualRequiredBalance = exception.RequiredBalance;

            Assert.AreEqual(expectedAccountBalance, actualAccountBalance);
            Assert.AreEqual(expectedRequiredBalance, actualRequiredBalance);
        }

        [Test]
        public void BuyStock_WithAccountUpdateFailure_ThrowsException()
        {
            Account buyer = CreateAccount(1000);
            _mockCurrencyPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ReturnsAsync(100);
            _mockAccountService.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<Account>())).Throws(new Exception());

            Assert.ThrowsAsync<Exception>(() => _buySService.BuyCurrency(buyer, It.IsAny<string>(), 1));
        }

        [Test]
        public async Task BuyStock_WithSuccessfulPurchase_ReturnsAccountWithNewTransaction()
        {
            int expectedTransactionCount = 1;
            Account buyer = CreateAccount(1000);
            _mockCurrencyPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ReturnsAsync(100);

            buyer = await _buySService.BuyCurrency(buyer, It.IsAny<string>(), 1);
            int actualTransactionCount = buyer.AssetTransactions.Count();

            Assert.AreEqual(expectedTransactionCount, actualTransactionCount);
        }

        [Test]
        public async Task BuyStock_WithSuccessfulPurchase_ReturnsAccountWithNewBalance()
        {
            decimal expectedBalance = 0;
            Account buyer = CreateAccount(100);
            _mockCurrencyPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ReturnsAsync(50);

            buyer = await _buySService.BuyCurrency(buyer, It.IsAny<string>(), 2);
            decimal actualBalance = buyer.Balance;

            Assert.AreEqual(expectedBalance, actualBalance);
        }

        private Account CreateAccount(decimal balance)
        {
            return new Account()
            {
                Balance = balance,
                AssetTransactions = new List<AssetTransaction>()
            };
        }
    }
}

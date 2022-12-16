using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trader.WPF.State.Accounts;
using Trader.WPF.ViewModels;
using TraderOop.Domain.Exceptions;
using TraderOop.Domain.Models;
using TraderOop.Domain.Services.TransactionServices;

namespace Trader.WPF.Commands
{
    public class SellCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly SellViewModel _sellViewModel;
        private readonly ISellService _sellService;
        private readonly IAccountStore _accountStore;

        public SellCommand(SellViewModel sellViewModel, ISellService sellService, IAccountStore accountStore)
        {
            _sellViewModel = sellViewModel;
            _sellService = sellService;
            _accountStore = accountStore;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            _sellViewModel.StatusMessage = string.Empty;
            _sellViewModel.ErrorMessage = string.Empty;

            try
            {
                string symbol = _sellViewModel.Symbol;
                int shares = _sellViewModel.SharesToSell;

                Account account = await _sellService.Sell(_accountStore.CurrentAccount, _sellViewModel.Symbol.ToUpper(), _sellViewModel.SharesToSell);

                _accountStore.CurrentAccount = account;

                _sellViewModel.SearchResultSymbol = string.Empty;
                _sellViewModel.StatusMessage = $"Sprzedano {shares} {symbol}.";
            }
            catch (InsufficientSharesException ex)
            {
                _sellViewModel.ErrorMessage = $"Chcesz sprzedać więcej walut niż posiadasz. Masz tylko {ex.AvailableShares}.";
            }
            catch (InvalidSymbolException)
            {
                _sellViewModel.ErrorMessage = "Nie znaleziono waluty o podanym symbolu.";
            }
            catch (Exception)
            {
                _sellViewModel.ErrorMessage = "Sprzedaż zakończona niepowodzeniem!";
            }
        }
    }
}

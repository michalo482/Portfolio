using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trader.WPF.Commands;
using Trader.WPF.State.Accounts;
using TraderOop.Domain.Services;
using TraderOop.Domain.Services.TransactionServices;

namespace Trader.WPF.ViewModels
{
    public class BuyViewModel : ViewModelBase, ISearchSymbolViewModel
    {
        private string _symbol;
        public string Symbol
        {
            get { return _symbol; }
            set
            {
                _symbol = value;
                OnPropertyChanged(nameof(Symbol));

            }
        }

        private string _searchResultSymbol = string.Empty;
        public string SearchResultSymbol
        {
            get
            {
                return _searchResultSymbol;
            }
            set
            {
                _searchResultSymbol = value;
                OnPropertyChanged(nameof(SearchResultSymbol));

            }
        }

        private decimal _currencyPrice;
        public decimal CurrencyPrice
        {
            get { return _currencyPrice; }
            set
            {
                _currencyPrice = value;
                OnPropertyChanged(nameof(CurrencyPrice));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private int _sharesToBuy;
        public int SharesToBuy
        {
            get { return _sharesToBuy; }
            set
            {
                _sharesToBuy = value; OnPropertyChanged(nameof(SharesToBuy));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public decimal TotalPrice
        {
            get
            {
                return SharesToBuy * CurrencyPrice;
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; set; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;

        }

        public string StatusMessage
        {
            set => StatusMessageViewModel.Message = value;

        }
        public MessageViewModel StatusMessageViewModel { get; set; }

        public ICommand SearchSymbolCommand { get; set; }
        public ICommand BuyCurrencyCommand { get; set; }

        public BuyViewModel(ICurrencyPriceService currencyPriceService, IBuyService buyService, IAccountStore accountStore)
        {
            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();

            SearchSymbolCommand = new SearchSymbolCommand(this, currencyPriceService);
            BuyCurrencyCommand = new BuyCurrencyCommand(this, buyService, accountStore);
        }
    }
}

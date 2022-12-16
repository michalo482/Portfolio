using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trader.WPF.Commands;
using Trader.WPF.State.Accounts;
using Trader.WPF.State.Assets;
using TraderOop.Domain.Services;
using TraderOop.Domain.Services.TransactionServices;

namespace Trader.WPF.ViewModels
{
    public class SellViewModel : ViewModelBase, ISearchSymbolViewModel
    {
        private StockViewModel _selectedCurrencies;
        public StockViewModel SelectedCurrencies
        {
            get
            {
                return _selectedCurrencies;
            }
            set
            {
                _selectedCurrencies = value;
                OnPropertyChanged(nameof(SelectedCurrencies));
            }
        }

        private string _symbol;
        public string Symbol => SelectedCurrencies?.Symbol;


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

        private int _sharesToSell;
        public int SharesToSell
        {
            get { return _sharesToSell; }
            set
            {
                _sharesToSell = value;
                OnPropertyChanged(nameof(SharesToSell));
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        public decimal TotalPrice => CurrencyPrice * SharesToSell;
        public AssetListingViewModel AssetListingViewModel { get; }

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

        public ICommand SearchSymbolCommand { get; }
        public ICommand SellCommand { get; }

        public SellViewModel(AssetStore assetStore, ICurrencyPriceService currencyPriceService, IAccountStore accountStore, ISellService sellService)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore);

            SearchSymbolCommand = new SearchSymbolCommand(this, currencyPriceService);
            SellCommand = new SellCommand(this, sellService, accountStore);

            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();
        }
    }
}

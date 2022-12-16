using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderOop.Domain.Models;
using TraderOop.Domain.Services;

namespace Trader.WPF.ViewModels
{
    public class CurrencyListingViewModel : ViewModelBase
    {
        private readonly ICurrencyService _currencyService;


        private CurrencyModel _usd;        
        public CurrencyModel USD 
        {
            get
            {
                return _usd;
            }
            set
            {
                _usd = value;
                OnPropertyChanged(nameof(USD));
            }
        }
        private CurrencyModel _chf;
        public CurrencyModel CHF
        {
            get
            {
                return _chf;
            }
            set
            {
                _chf = value;
                OnPropertyChanged(nameof(CHF));
            }
        }
        private CurrencyModel _gbp;
        public CurrencyModel GBP { 
            get
            {
                return _gbp;
            }
            set
            {
                _gbp = value;
                OnPropertyChanged(nameof(GBP));
            } 
        }

        public CurrencyListingViewModel(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        

        public static CurrencyListingViewModel LoadCurrencyViewModel(ICurrencyService currencyService)
        {
            CurrencyListingViewModel currencyViewModel = new CurrencyListingViewModel(currencyService);
            currencyViewModel.LoadCurrencies();
            return currencyViewModel;
        }

        private void LoadCurrencies()
        {
            _currencyService.GetCurrency(CurrencyCode.USD).ContinueWith(task =>
            {
                if(task.Exception == null)
                {
                    USD = task.Result;
                }
            } );
            _currencyService.GetCurrency(CurrencyCode.CHF).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    CHF = task.Result;
                }
            }); ;
            _currencyService.GetCurrency(CurrencyCode.GBP).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    GBP = task.Result;
                }
            }); ;
        }
    }
}

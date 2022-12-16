using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderOop.Domain.Services;

namespace Trader.WPF.ViewModels.Factories
{
    public class CurrencyListingViewModelFactory : ITraderViewModelFactory<CurrencyListingViewModel>
    {

        private readonly ICurrencyService _currencyService;

        public CurrencyListingViewModelFactory(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        public CurrencyListingViewModel CreateViewModel()
        {
            return CurrencyListingViewModel.LoadCurrencyViewModel(_currencyService);
        }
    }
}

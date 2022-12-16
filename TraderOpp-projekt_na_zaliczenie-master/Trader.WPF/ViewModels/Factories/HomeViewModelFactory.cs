using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.WPF.ViewModels.Factories
{
    public class HomeViewModelFactory //: ITraderViewModelFactory<HomeViewModel>
    {

        private readonly ITraderViewModelFactory<CurrencyListingViewModel> _currencyViewModelFactory;

        public HomeViewModelFactory(ITraderViewModelFactory<CurrencyListingViewModel> currencyViewModelFactory)
        {
            _currencyViewModelFactory = currencyViewModelFactory;
        }

        /*public HomeViewModel CreateViewModel()
        {
            return new HomeViewModel(_currencyViewModelFactory.CreateViewModel());
        }*/
    }

}

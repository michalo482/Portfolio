using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.WPF.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public AssetSummaryViewModel AssetSummaryViewModel { get; }
        public CurrencyListingViewModel CurrencyListingViewModel { get; }

        public HomeViewModel(AssetSummaryViewModel assetSummaryViewModel, CurrencyListingViewModel currencyListingViewModel)
        {
            CurrencyListingViewModel = currencyListingViewModel;
            AssetSummaryViewModel = assetSummaryViewModel;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.WPF.State.Assets;

namespace Trader.WPF.ViewModels
{
    public class PortfolioViewModel : ViewModelBase
    {
       

        

        public AssetListingViewModel AssetListingViewModel { get; }

        public PortfolioViewModel(AssetStore assetStore)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore);
        }
    }
}

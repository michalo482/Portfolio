using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.WPF.State.Assets;

namespace Trader.WPF.ViewModels
{
    public class AssetSummaryViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;

        
        public AssetListingViewModel AssetListingViewModel { get; }
        //public IEnumerable<StockViewModel> Assets => _assets;

        public decimal AccountBalance => _assetStore.AccountBalance;
        
        

        public AssetSummaryViewModel(AssetStore assetStore)
        {
            AssetListingViewModel = new AssetListingViewModel(assetStore, assets => assets.Take(3));
            _assetStore = assetStore;

            _assetStore.StateChanged += AssetStore_StateChanged;

        }

        private void AssetStore_StateChanged()
        {
            OnPropertyChanged(nameof(AccountBalance));
        }
     
    }
}

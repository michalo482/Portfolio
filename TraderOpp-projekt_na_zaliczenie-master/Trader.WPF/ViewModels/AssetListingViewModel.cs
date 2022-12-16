using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.WPF.State.Assets;

namespace Trader.WPF.ViewModels
{
    public class AssetListingViewModel : ViewModelBase
    {
        private readonly AssetStore _assetStore;
        private readonly Func<IEnumerable<StockViewModel>, IEnumerable<StockViewModel>> _filterAssets;
        private readonly ObservableCollection<StockViewModel> _assets;
        public IEnumerable<StockViewModel> Assets => _assets;


        public AssetListingViewModel(AssetStore assetStore, Func<IEnumerable<StockViewModel>, IEnumerable<StockViewModel>> filterAssets)
        {
            _assetStore = assetStore;
            _filterAssets = filterAssets;
            _assets = new ObservableCollection<StockViewModel>();

            _assetStore.StateChanged += AssetStore_StateChanged;

            ResetAssets();
        }

        public AssetListingViewModel(AssetStore assetStore) : this(assetStore, assets => assets)
        {
           
        }

        private void AssetStore_StateChanged()
        {
            ResetAssets();
        }

        private void ResetAssets()
        {
            IEnumerable<StockViewModel> assetsViewModel = (IEnumerable<StockViewModel>)_assetStore.AssetTransactions
                .GroupBy(t => t.Stock.Symbol)
                .Select(g => new StockViewModel(g.Key, g.Sum(t => t.IsPurchase ? t.Shares : -t.Shares)))
                .Where(s => s.Shares > 0)
                .OrderByDescending(s => s.Shares);


            assetsViewModel = _filterAssets(assetsViewModel);

            _assets.Clear();

            foreach (StockViewModel asset in assetsViewModel)
            {
                _assets.Add(asset);
            }
        }
    }
}

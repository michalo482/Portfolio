using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.FinancialModelingPrepAPI.Services;
using Trader.WPF.State.Navigators;

namespace Trader.WPF.ViewModels.Factories
{
    /// <summary>
    /// pobiera serwisy i kiedy chcemy dany ViewModel wołamy CreateViewModel,
    /// dostajemy dany ViewModel i nie musimy wiedzieć jak on działa
    /// fabryki widoków są wstrzykiwane w App.xaml.cs przez Dependency Injection
    /// </summary>
    public class TradeViewModelAbstractFactory : ITraderViewModelAbstractFactory
    {

        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<PortfolioViewModel> _createPortfolioViewModel;
        private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
        private readonly CreateViewModel<BuyViewModel> _createBuyViewModel;
        private readonly CreateViewModel<SellViewModel> _createSellViewModel;

        public TradeViewModelAbstractFactory(CreateViewModel<HomeViewModel> createHomeViewModel,
            CreateViewModel<PortfolioViewModel> createPortfolioViewModel, CreateViewModel<LoginViewModel> createLoginViewModel,
            CreateViewModel<BuyViewModel> createBuyViewModel, CreateViewModel<SellViewModel> createSellViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createPortfolioViewModel = createPortfolioViewModel;
            _createLoginViewModel = createLoginViewModel;
            _createBuyViewModel = createBuyViewModel;
            _createSellViewModel = createSellViewModel;
        }

        /// <summary>
        /// tworzy ViewModel dla danego ViewType
        /// </summary>
        /// <param name="viewType">enum odpowiadający poszczególnym ViewModelom wyświetlanym w aplikacji</param>
        /// <returns>zwraca ViewModel odpowiadający typowi wyłanemu</returns>
        /// <exception cref="ArgumentException"></exception>
        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Login:
                    return _createLoginViewModel();
                case ViewType.Home:
                    return _createHomeViewModel();
                case ViewType.Portfolio:
                    return _createPortfolioViewModel();
                case ViewType.Buy:
                    return _createBuyViewModel();
                case ViewType.Sell:
                    return _createSellViewModel();
                default:
                    throw new ArgumentException("No ViewModel for that ViewType", "viewType");
            }
        }
    }
}

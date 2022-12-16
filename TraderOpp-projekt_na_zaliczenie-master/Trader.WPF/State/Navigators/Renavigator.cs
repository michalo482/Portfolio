using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.WPF.ViewModels;
using Trader.WPF.ViewModels.Factories;

namespace Trader.WPF.State.Navigators
{
    public class Renavigator<TViewModel> : IRenavigator where TViewModel : ViewModelBase
    {
        private readonly INavigator _navigator;
        private readonly CreateViewModel<TViewModel> _createViewiewModel;

        public Renavigator(INavigator navigator, CreateViewModel<TViewModel> createViewiewModel)
        {
            _navigator = navigator;
            _createViewiewModel = createViewiewModel;
        }

        public void Renavigate()
        {
            _navigator.CurrentViewModel = _createViewiewModel();
        }
    }
}

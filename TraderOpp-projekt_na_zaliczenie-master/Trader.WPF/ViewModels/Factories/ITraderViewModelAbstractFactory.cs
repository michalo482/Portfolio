using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.WPF.State.Navigators;

namespace Trader.WPF.ViewModels.Factories
{
    public interface ITraderViewModelAbstractFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.WPF.ViewModels.Factories
{
    public interface ITraderViewModelFactory<T> where T : ViewModelBase
    {
        T CreateViewModel();
    }
}

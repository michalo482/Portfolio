using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trader.WPF.ViewModels;

namespace Trader.WPF.State.Navigators
{
    public enum ViewType
    {
        Login,
        Home,
        Portfolio,
        Buy,
        Sell
        
    }
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChange;
    }
}

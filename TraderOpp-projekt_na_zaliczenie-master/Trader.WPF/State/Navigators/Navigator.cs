using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trader.WPF.Commands;
using Trader.WPF.Models;
using Trader.WPF.ViewModels;
using Trader.WPF.ViewModels.Factories;

namespace Trader.WPF.State.Navigators
{
    public class Navigator : INavigator
    {
        private ViewModelBase _currentViewModel;
        public event Action StateChange;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                _currentViewModel = value;
                StateChange?.Invoke();
            }
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trader.WPF.Commands;
using Trader.WPF.State.Authenticators;
using Trader.WPF.State.Navigators;
using Trader.WPF.ViewModels.Factories;

namespace Trader.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ITraderViewModelAbstractFactory _traderViewModelFactory;
        private readonly INavigator _navigator;
        private readonly IAuthenticator _authenticator;

        public bool IsLoggedIn => _authenticator.IsLoggedIn;
        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

        public ICommand UpdateCurrentViewModelCommand { get; }

        public MainViewModel(INavigator navigator, ITraderViewModelAbstractFactory traderViewModelFactory, IAuthenticator authenticator)
        {
            _navigator = navigator;
            _traderViewModelFactory = traderViewModelFactory;
            _authenticator = authenticator;

            _navigator.StateChange += Navigator_StateChanged;
            _authenticator.StateChange += Authenticator_StateChanged;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _traderViewModelFactory);
            
            UpdateCurrentViewModelCommand.Execute(ViewType.Login);
        }

        private void Authenticator_StateChanged()
        {
            OnPropertyChanged(nameof(IsLoggedIn));
        }

        private void Navigator_StateChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}

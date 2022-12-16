using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trader.WPF.Commands;
using Trader.WPF.State.Authenticators;
using Trader.WPF.State.Navigators;

namespace Trader.WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public MessageViewModel ErrorMessageViewModel { get; set; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;

        }

        public string StatusMessage
        {
            set => StatusMessageViewModel.Message = value;

        }
        public MessageViewModel StatusMessageViewModel { get; set; }

        public ICommand LoginCommand { get; }
        public ICommand ViewRegisterCommand { get; }
        public LoginViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator, IRenavigator registerRenavigator)
        {
            ErrorMessageViewModel = new MessageViewModel();
            StatusMessageViewModel = new MessageViewModel();

            LoginCommand = new LoginCommand(authenticator, this, loginRenavigator);
            ViewRegisterCommand = new RenavigateCommand(registerRenavigator);
        }
    }
}

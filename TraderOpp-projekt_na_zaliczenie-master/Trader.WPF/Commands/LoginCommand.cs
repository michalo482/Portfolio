using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trader.WPF.State.Authenticators;
using Trader.WPF.State.Navigators;
using Trader.WPF.ViewModels;
using Trader.WPF.ViewModels.Factories;
using TraderOop.Domain.Exceptions;

namespace Trader.WPF.Commands
{
    public class LoginCommand : ICommand
    {
        private readonly IAuthenticator _authenticator;
        private readonly LoginViewModel _loginViewModel;
        private readonly IRenavigator _renavigator;
        

        public LoginCommand(IAuthenticator authenticator, LoginViewModel loginViewModel, IRenavigator renavigator)
        {
            _authenticator = authenticator;
            _loginViewModel = loginViewModel;
            _renavigator = renavigator;

        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            try
            {
                await _authenticator.Login(_loginViewModel.Username, _loginViewModel.Password);
                _renavigator.Renavigate();
            }
            catch (UserNotFoundException)
            {
                _loginViewModel.ErrorMessage = "Nazwy użytkownika nie ma w bazie.";
            }
            catch (InvalidPasswordException)
            {
                _loginViewModel.ErrorMessage = "Hasło jest nieprawidłowe.";
            }
            catch (Exception)
            {
                _loginViewModel.ErrorMessage = "Wystąpił nieoczekiwany błąd.";
            }

        }
    }
}

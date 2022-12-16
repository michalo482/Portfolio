using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trader.WPF.State.Authenticators;
using Trader.WPF.State.Navigators;
using Trader.WPF.ViewModels;
using TraderOop.Domain.Services.AuthenticationServices;

namespace Trader.WPF.Commands
{
    public class RegisterCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private readonly RegisterViewModel _registerViewModel;
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public RegisterCommand(RegisterViewModel registerViewModel, IAuthenticator authenticator, IRenavigator renavigator)
        {
            _registerViewModel = registerViewModel;
            _authenticator = authenticator;
            _renavigator = renavigator;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            _registerViewModel.ErrorMessage = string.Empty;
            try
            {
                RegistrationResult result = await _authenticator.Register(_registerViewModel.Email, _registerViewModel.Username, _registerViewModel.Password, _registerViewModel.ConfirmPassword);
                switch (result)
                {
                    case RegistrationResult.Success:
                        _renavigator.Renavigate();
                        break;
                    case RegistrationResult.EmailAlreadyExists:
                        _registerViewModel.ErrorMessage = "Email już zarejestrowany";
                        break;
                    case RegistrationResult.UsernameAlreadyExists:
                        _registerViewModel.ErrorMessage = "Nazwa użytkownika już zarejestrowana";
                        break;
                    case RegistrationResult.PasswordsDoNotMatch:
                        _registerViewModel.ErrorMessage = "Hasła nie są identyczne";
                        break;
                    default:
                        _registerViewModel.ErrorMessage = "Nieznany błąd";
                        break;
                }
            }
            catch (Exception)
            {
                _registerViewModel.ErrorMessage = "Nieznany błąd";
            }
        }
    }
}

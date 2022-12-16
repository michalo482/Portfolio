using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderOop.Domain.Models;
using TraderOop.Domain.Exceptions;
using TraderOop.Domain.Services.AuthenticationServices;

namespace Trader.WPF.State.Authenticators
{
    public interface IAuthenticator
    {
        Account CurrenctAccount { get; }
        bool IsLoggedIn { get; }

        event Action StateChange;
        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);
        /// <summary>
        /// Logowanie przy użyciu nazwy użytkownika i hasła
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <exception cref="UserNotFoundException">jeśli użytkownika nie istnieje w bazie</exception>
        /// <exception cref="InvalidPasswordException">jeśli podane hasło nie pasuje do tego w bazie</exception>
        /// <exception cref="Exception">jeśli cos pójdzie nie tak</exception>
        Task Login(string username, string password);
        void Logout();
    }
}

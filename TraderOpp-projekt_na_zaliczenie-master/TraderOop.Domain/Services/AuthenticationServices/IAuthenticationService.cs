using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderOop.Domain.Models;
using TraderOop.Domain.Exceptions;

namespace TraderOop.Domain.Services.AuthenticationServices
{
    public enum RegistrationResult
    {
        Success,
        PasswordsDoNotMatch,
        EmailAlreadyExists,
        UsernameAlreadyExists
    }
    
    public interface IAuthenticationService
    {
        Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);
        /// <summary>
        /// Logowanie za pomocą nazwy użytkownika i hasła
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Konto użytkownika</returns>
        /// <exception cref="UserNotFoundException">jeśli użytkownika nie istnieje w bazie</exception>
        /// <exception cref="InvalidPasswordException">jeśli podane hasło nie pasuje do tego w bazie</exception>
        /// <exception cref="Exception">jeśli cos pójdzie nie tak</exception>
        Task<Account> Login(string username, string password);
    }
}

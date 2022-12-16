using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.WPF.Models;
using Trader.WPF.State.Accounts;
using TraderOop.Domain.Models;
using TraderOop.Domain.Services.AuthenticationServices;

namespace Trader.WPF.State.Authenticators
{
    public class Authenticator : IAuthenticator
    {

        private readonly IAuthenticationService _authenticationService;
        private readonly IAccountStore _accountStore;

        public Authenticator(IAuthenticationService authenticationService, IAccountStore accountStore)
        {
            _authenticationService = authenticationService;
            _accountStore = accountStore;
        }
        public event Action StateChange;
        public Account CurrenctAccount { 
            get
            {
                return _accountStore.CurrentAccount;
            }
            private set
            {
                _accountStore.CurrentAccount = value;
                StateChange?.Invoke();

            }
        }


        public bool IsLoggedIn => CurrenctAccount != null;

        public async Task Login(string username, string password)
        {
            
                CurrenctAccount = await _authenticationService.Login(username, password);
           
        }

            public void Logout()
        {
            CurrenctAccount = null;
        }

        public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
        {
            return await _authenticationService.Register(email, username, password, confirmPassword);
        }
    }
}

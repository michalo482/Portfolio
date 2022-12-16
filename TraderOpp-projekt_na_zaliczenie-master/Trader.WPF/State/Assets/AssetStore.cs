using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.WPF.State.Accounts;
using TraderOop.Domain.Models;

namespace Trader.WPF.State.Assets
{
    public class AssetStore
    {
        private readonly IAccountStore _accountStore;

        public decimal AccountBalance => _accountStore.CurrentAccount?.Balance ?? 0;
        public IEnumerable<AssetTransaction> AssetTransactions => _accountStore.CurrentAccount?.AssetTransactions ?? Enumerable.Empty<AssetTransaction>();

        public event Action StateChanged;
        public AssetStore(IAccountStore accountStore)
        {
            _accountStore = accountStore;

            _accountStore.StateChanged += OnStateChanged;
        }

        private void OnStateChanged()
        {
            StateChanged?.Invoke();
        }
    }
}

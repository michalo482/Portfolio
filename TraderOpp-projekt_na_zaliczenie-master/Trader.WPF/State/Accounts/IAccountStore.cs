using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderOop.Domain.Models;

namespace Trader.WPF.State.Accounts
{
    public interface IAccountStore
    {
        Account CurrentAccount { get; set; }
        event Action StateChanged;
    }
}

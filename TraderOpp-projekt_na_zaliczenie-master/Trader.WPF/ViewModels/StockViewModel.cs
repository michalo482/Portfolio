using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trader.WPF.ViewModels
{
    public  class StockViewModel : ViewModelBase
    {
        public StockViewModel(string symbol, int shares)
        {
            Symbol = symbol;
            Shares = shares;
        }

        public string Symbol { get; }
        public int Shares { get; }

    }
}

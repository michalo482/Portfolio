using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraderOop.Domain.Models;

namespace TraderOop.Domain.Services
{
    public interface ICurrencyService
    {
        Task<CurrencyModel> GetCurrency(CurrencyCode currencyCode);
    }
}

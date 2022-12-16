using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderOop.Domain.Models
{
    public enum CurrencyCode
    {
        USD,
        CHF,
        GBP
        
    }

    public class CurrencyModel
    {
        public string Table { get; set; }
        public string Currency { get; set; }        
        public CurrencyCode Code { get; set; }  
        public List<Rate> Rates { get; set; }
        
        public decimal Mid 
        { 
            get
            {
                return Rates.First().Mid;
            }
        }
        
        public string EffectiveDate
        {
            get
            {
                return Rates.First().EffectiveDate;
            }
        }
    }
}

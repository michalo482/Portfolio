using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderOop.Domain.Models
{
    public class Account : DomainObject
    {       
        public User AccountHolder { get; set; }
        public decimal Balance { get; set; }
        public ICollection<AssetTransaction> AssetTransactions { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TraderOop.Domain.Exceptions
{
    public class InsufficientFundsException : Exception
    {

        public decimal AccountBalance { get; set; }
        public decimal RequiredBalance { get; set; }
        public InsufficientFundsException(decimal balance, decimal requiredBalance)
        {
            AccountBalance = balance;
            RequiredBalance = requiredBalance;
        }

        public InsufficientFundsException(decimal balance, decimal requiredBalance, string? message) : base(message)
        {
            AccountBalance = balance;
            RequiredBalance = requiredBalance;
        }

        public InsufficientFundsException(decimal balance, decimal requiredBalance, string? message, Exception? innerException) : base(message, innerException)
        {
            AccountBalance = balance;
            RequiredBalance = requiredBalance;
        }

    }
}

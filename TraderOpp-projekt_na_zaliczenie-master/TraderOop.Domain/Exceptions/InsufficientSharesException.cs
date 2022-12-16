using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TraderOop.Domain.Exceptions
{
    public class InsufficientSharesException : Exception
    {
        public string Symbol { get; }
        public int AvailableShares { get; }
        public int RequiredShares { get; }

        public InsufficientSharesException(string symbol, int availableShares, int requiredShares)
        {
            Symbol = symbol;
            AvailableShares = availableShares;
            RequiredShares = requiredShares;
        }

        public InsufficientSharesException(string? message, string symbol, int availableShares, int requiredShares) : base(message)
        {
            Symbol = symbol;
            AvailableShares = availableShares;
            RequiredShares = requiredShares;
        }

        public InsufficientSharesException(string? message, Exception? innerException, string symbol, int availableShares, int requiredShares) : base(message, innerException)
        {
            Symbol = symbol;
            AvailableShares = availableShares;
            RequiredShares = requiredShares;
        }

    }
}

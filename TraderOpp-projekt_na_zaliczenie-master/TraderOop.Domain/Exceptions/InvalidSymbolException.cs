using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraderOop.Domain.Exceptions
{
    public class InvalidSymbolException : Exception
    {
        public string Code { get; }

        public InvalidSymbolException(string code)
        {
            Code = code;
        }
        public InvalidSymbolException(string code, string message) : base(message)
        {
            Code = code;
        }

        public InvalidSymbolException(string code, string? message, Exception? innerException) : base(message, innerException)
        {
            Code = code;
        }
    }

}

using Digipolis.Codetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digipolis.Codetable.Business
{
    public class CodetableBusinessValidationException : Exception
    {
        public CodetableBusinessValidationException(Error error)
        {
            this.Error = error;
        }

        public CodetableBusinessValidationException(string message, params object[] args) : this(new Error())
        {
            this.Error = new Error(message, args);
        }

        public CodetableBusinessValidationException(IEnumerable<string> messages)
        {
            this.Error = new Error(messages);
        }

        public Error Error { get; private set; }
    }
}

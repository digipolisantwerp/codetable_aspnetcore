using Toolbox.Codetable.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Toolbox.Codetable.Business
{
    public class CodetabelBusinessValidationException : Exception
    {
        public CodetabelBusinessValidationException(Error error)
        {
            this.Error = error;
        }

        public CodetabelBusinessValidationException(string message, params object[] args) : this(new Error())
        {
            this.Error = new Error(message, args);
        }

        public CodetabelBusinessValidationException(IEnumerable<string> messages)
        {
            this.Error = new Error(messages);
        }

        public Error Error { get; private set; }
    }
}

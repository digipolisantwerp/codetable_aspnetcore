using System.Collections.Generic;
using System.Reflection;

namespace Digipolis.Codetable
{
    public interface ICodetableProvider
    {
        IEnumerable<CodetableControllerInfo> Codetables { get; }

        void Load(Assembly assembly);
    }
}

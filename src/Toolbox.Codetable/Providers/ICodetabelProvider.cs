using System;
using System.Collections.Generic;
using System.Reflection;

namespace Toolbox.Codetable
{
    public interface ICodetableProvider
    {
        IEnumerable<CodetableControllerInfo> Codetables { get; }

        void Load(Assembly assembly);
    }
}

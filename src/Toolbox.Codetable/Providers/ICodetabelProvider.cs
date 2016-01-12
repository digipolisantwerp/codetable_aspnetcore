using System;
using System.Collections.Generic;
using System.Reflection;

namespace Toolbox.Codetable
{
    public interface ICodetabelProvider
    {
        IEnumerable<CodetabelControllerInfo> Codetabellen { get; }

        void Load(Assembly assembly);
    }
}

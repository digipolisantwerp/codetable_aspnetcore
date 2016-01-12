using System;

namespace Toolbox.Codetable
{
    public interface IValueBuilder
    {
        string GetValueOrDefault(string value, string defaultValue);
    }
}

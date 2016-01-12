using System;

namespace Toolbox.Codetable.Internal
{
    public class ControllerValueBuilder : IValueBuilder
    {
        public string GetValueOrDefault(string value, string defaultValue)
        {
            var valuePart = value.Replace("Controller", String.Empty);
            string controller = "[controller]";

            int index = defaultValue.IndexOf(controller, StringComparison.OrdinalIgnoreCase);
            if (index < 0)
            {
                return defaultValue;
            }            
            return defaultValue.Remove(index, controller.Length).Insert(index, valuePart);            
        }
    }
}

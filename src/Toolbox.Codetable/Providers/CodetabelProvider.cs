using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Toolbox.Common.Helpers;
using Toolbox.Common.Validation;
using Microsoft.AspNet.Mvc;

namespace Toolbox.Codetable.Internal
{
    public class CodetableProvider : ICodetableProvider
    {
        public CodetableProvider(IValueBuilder valueBuilder)
        {
            ArgumentValidator.AssertNotNull(valueBuilder, nameof(valueBuilder));
            _valueBuilder = valueBuilder;
            this.Codetables = new CodetableControllerInfo[] { };
        }

        private readonly IValueBuilder _valueBuilder;

        public IEnumerable<CodetableControllerInfo> Codetables { get; private set; }

        public void Load(Assembly assembly)
        {
            ArgumentValidator.AssertNotNull(assembly, nameof(assembly));

            var types = ReflectionHelper.GetTypesWithAttribute<CodetableControllerAttribute>(assembly, true);
            if ( types.Count() == 0 )
                this.Codetables = new CodetableControllerInfo[] { };
            else
                this.Codetables = GetCodetableInfoLijst(assembly, types);
        }

        private IEnumerable<CodetableControllerInfo> GetCodetableInfoLijst(Assembly assembly, IEnumerable<Type> types)
        {
            var lijst = new List<CodetableControllerInfo>();
            foreach ( var type in types )
            {
                var name = GetName(type);
                var route = GetRoute(type);
                lijst.Add(new CodetableControllerInfo(name, route));
            }
            return lijst.ToArray();
        }

        private string GetName(Type type)
        {
            var codetableAttrib = ReflectionHelper.GetAttributeFrom<CodetableControllerAttribute>(type);
            var name = _valueBuilder.GetValueOrDefault(type.Name, codetableAttrib.Name);
            return name;
        }

        private string GetRoute(Type type)
        {
            string template;
            var routeAttrib = ReflectionHelper.GetAttributeFrom<RouteAttribute>(type);
            template = routeAttrib == null ? "[controller]" : routeAttrib.Template;
            var route = _valueBuilder.GetValueOrDefault(type.Name, template);
            return route;
        }
    }
}

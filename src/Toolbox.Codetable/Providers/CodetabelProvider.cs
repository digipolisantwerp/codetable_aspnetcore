using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Toolbox.Common.Helpers;
using Toolbox.Common.Validation;
using Microsoft.AspNet.Mvc;

namespace Toolbox.Codetable.Internal
{
    public class CodetabelProvider : ICodetabelProvider
    {
        public CodetabelProvider(IValueBuilder valueBuilder)
        {
            ArgumentValidator.AssertNotNull(valueBuilder, nameof(valueBuilder));
            _valueBuilder = valueBuilder;
            this.Codetabellen = new CodetabelControllerInfo[] { };
        }

        private readonly IValueBuilder _valueBuilder;

        public IEnumerable<CodetabelControllerInfo> Codetabellen { get; private set; }

        public void Load(Assembly assembly)
        {
            ArgumentValidator.AssertNotNull(assembly, nameof(assembly));

            var types = ReflectionHelper.GetTypesWithAttribute<CodetabelControllerAttribute>(assembly, true);
            if ( types.Count() == 0 )
                this.Codetabellen = new CodetabelControllerInfo[] { };
            else
                this.Codetabellen = GetCodetabelInfoLijst(assembly, types);
        }

        private IEnumerable<CodetabelControllerInfo> GetCodetabelInfoLijst(Assembly assembly, IEnumerable<Type> types)
        {
            var lijst = new List<CodetabelControllerInfo>();
            foreach ( var type in types )
            {
                var naam = GetNaam(type);
                var route = GetRoute(type);
                lijst.Add(new CodetabelControllerInfo(naam, route));
            }
            return lijst.ToArray();
        }

        private string GetNaam(Type type)
        {
            var codetabelAttrib = ReflectionHelper.GetAttributeFrom<CodetabelControllerAttribute>(type);
            var naam = _valueBuilder.GetValueOrDefault(type.Name, codetabelAttrib.Naam);
            return naam;
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

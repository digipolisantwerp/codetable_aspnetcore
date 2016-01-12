using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Toolbox.Codetable.Errors;
using Toolbox.Common.Validation;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Entity;
using Microsoft.AspNet.Mvc.Abstractions;
using Microsoft.AspNet.Mvc.Infrastructure;

namespace Toolbox.Codetable
{
    public static class CodetabelAppBuilderExtensions
    {
        /// <summary>
        /// Voegt het CodetabelDiscovery framework toe aan de toepassing zodat de lijst van codetabellen opvraagbaar is.
        /// </summary>
        /// <param name="app">De IApplicationBuilder van de toepassing.</param>        
        /// <returns>De IApplicationBuilder de toepassing.</returns>
        public static IApplicationBuilder UseCodetabelDiscovery(this IApplicationBuilder app)
        {
            var provider = app.ApplicationServices.GetService<ICodetabelProvider>();
            if (provider == null) throw ExceptionProvider.CodetabelProviderNietGeregistreerd();
            var options = app.ApplicationServices.GetService<CodetabelDiscoveryOptions>();
            var assembly = options.ControllerAssembly == null ? Assembly.GetCallingAssembly() : options.ControllerAssembly;
            provider.Load(assembly);

            if (!String.IsNullOrWhiteSpace(options.Route)) SetRoute(app, options.Route);

            return app;
        }

        private static void SetRoute(IApplicationBuilder app, string route)
        {
            if (route.ToLower() != Routes.CodetabelProviderController)
            {
                var controllers = GetCodetabelProviderControllers(app);

                var routeBuilder = app.ApplicationServices.GetService<ICodetabelDiscoveryRouteBuilder>();
                if (routeBuilder == null) throw ExceptionProvider.RouteBuilderNietGeregistreerd();

                routeBuilder.SetRoute(controllers, route);
            }
        }

        private static IEnumerable<ActionDescriptor> GetCodetabelProviderControllers(IApplicationBuilder app)
        {
            var actionDescriptorsProvider = app.ApplicationServices.GetService<IActionDescriptorsCollectionProvider>();
            if (actionDescriptorsProvider == null) throw ExceptionProvider.MvcNietGeregistreerdGeenActionDescriptorsProvider();

            var controllers = from c in actionDescriptorsProvider.ActionDescriptors.Items
                              where c.DisplayName.ToLower().StartsWith("Toolbox.Codetable.codetabelprovider", StringComparison.Ordinal)
                              select c;

            if (controllers.Count() == 0) throw ExceptionProvider.MvcNietGeregistreerdGeenControllers();

            return controllers;
        }

       
    }
}
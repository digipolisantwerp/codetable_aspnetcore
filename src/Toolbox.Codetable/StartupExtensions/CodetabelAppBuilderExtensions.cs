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
    public static class CodetableAppBuilderExtensions
    {
        /// <summary>
        /// Adds the CodetableDiscovery framework to the application so that the list of codetables can be requested.
        /// </summary>
        /// <param name="app">The IApplicationBuilder of the application.</param>        
        /// <returns>The IApplicationBuilder the applpication.</returns>
        public static IApplicationBuilder UseCodetableDiscovery(this IApplicationBuilder app)
        {
            var provider = app.ApplicationServices.GetService<ICodetableProvider>();
            if (provider == null) throw ExceptionProvider.CodetableProviderNotRegistered();
            var options = app.ApplicationServices.GetService<CodetableDiscoveryOptions>();
            var assembly = options.ControllerAssembly == null ? Assembly.GetCallingAssembly() : options.ControllerAssembly;
            provider.Load(assembly);

            if (!String.IsNullOrWhiteSpace(options.Route)) SetRoute(app, options.Route);

            return app;
        }

        private static void SetRoute(IApplicationBuilder app, string route)
        {
            if (route.ToLower() != Routes.CodetableProviderController)
            {
                var controllers = GetCodetableProviderControllers(app);

                var routeBuilder = app.ApplicationServices.GetService<ICodetableDiscoveryRouteBuilder>();
                if (routeBuilder == null) throw ExceptionProvider.RouteBuilderNotRegistered();

                routeBuilder.SetRoute(controllers, route);
            }
        }

        private static IEnumerable<ActionDescriptor> GetCodetableProviderControllers(IApplicationBuilder app)
        {
            var actionDescriptorsProvider = app.ApplicationServices.GetService<IActionDescriptorsCollectionProvider>();
            if (actionDescriptorsProvider == null) throw ExceptionProvider.MvcNotRegisteredNoActionDescriptorsProvider();

            var controllers = from c in actionDescriptorsProvider.ActionDescriptors.Items
                              where c.DisplayName.ToLower().StartsWith("Toolbox.Codetable.codetableprovider", StringComparison.Ordinal)
                              select c;

            if (controllers.Count() == 0) throw ExceptionProvider.MvcNotRegisteredNoControllers();

            return controllers;
        }

       
    }
}
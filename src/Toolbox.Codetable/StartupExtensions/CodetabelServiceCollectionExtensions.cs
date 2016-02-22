using System;
using Toolbox.Codetable.Internal;
using Microsoft.Extensions.DependencyInjection;
using Toolbox.Common.Validation;
using Toolbox.Codetable.Errors;
using System.Reflection;
using Microsoft.AspNet.Mvc;
using System.Collections.Generic;
using System.Linq;
using Toolbox.Codetable.Business;
//using Toolbox.DataAccess.StartupExtentions;
using Toolbox.DataAccess.Entities;
using Toolbox.DataAccess.Options;

namespace Toolbox.Codetable
{
    public static class CodetableServiceCollectionExtensions
    {
        /// <summary>
        /// Configures the CodetableDiscovery framework.
        /// </summary>
        /// <param name="services">The IServiceCollection of the application.</param>
        /// <param name="setupAction">The action to perform setup on the CodetableDiscoveryOptions object.</param>
        /// <returns></returns>
        public static IServiceCollection AddCodetableDiscovery(this IServiceCollection services, Action<CodetableDiscoveryOptions> setupAction = null)
        {
            if (setupAction == null)
                setupAction = options => { };

            RegisterControllerDiscovery(services, setupAction);
            
            return services;
        }


        private static void RegisterControllerDiscovery(IServiceCollection services, Action<CodetableDiscoveryOptions> setupAction)
        {

            services.AddSingleton(typeof(IServiceCollection), (o) => { return services; });

            services.AddSingleton<IValueBuilder, ControllerValueBuilder>();      
            services.AddSingleton<ICodetableProvider, CodetableProvider>();
            services.AddSingleton<ICodetableDiscoveryRouteBuilder, CodetableDiscoveryRouteBuilder>();

            services.Configure(setupAction);

            services.AddTransient(typeof(ICodetableWriter<>), typeof(CodeTabelWriter<>));
            services.AddTransient(typeof(ICodetableReader<>), typeof(CodetableReader<>));
        }



        
    }
}

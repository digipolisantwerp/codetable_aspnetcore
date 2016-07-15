using Digipolis.Codetable.Business;
using Digipolis.Codetable.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Digipolis.Codetable
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

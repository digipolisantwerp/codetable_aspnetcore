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
    public static class CodetabelServiceCollectionExtensions
    {
        /// <summary>
        /// Configureert het CodetabelDiscovery framework.
        /// </summary>
        /// <param name="services">De IServiceCollection van de toepassing.</param>
        /// <returns>De IServicesCollection van de toepassing.</returns>
        public static IServiceCollection AddCodetabelDiscovery(this IServiceCollection services, CodetabelDiscoveryOptions options)
        {
            ArgumentValidator.AssertNotNull(options, nameof(options));

            RegisterControllerDiscovery(services, options);
            
            return services;
        }


        private static void RegisterControllerDiscovery(IServiceCollection services, CodetabelDiscoveryOptions options)
        {

            services.AddSingleton(typeof(IServiceCollection), (o) => { return services; });

            services.AddSingleton<IValueBuilder, ControllerValueBuilder>();      
            services.AddSingleton<ICodetabelProvider, CodetabelProvider>();
            services.AddSingleton<ICodetabelDiscoveryRouteBuilder, CodetabelDiscoveryRouteBuilder>();
            services.AddInstance(options);

            services.AddTransient(typeof(ICodetabelWriter<>), typeof(CodeTabelWriter<>));
            services.AddTransient(typeof(ICodetabelReader<>), typeof(CodetabelReader<>));


        }



        
    }
}

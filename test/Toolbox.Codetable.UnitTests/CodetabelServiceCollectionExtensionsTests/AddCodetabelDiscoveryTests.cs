using System;
using System.Linq;
using Toolbox.Codetable.Internal;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Toolbox.DataAccess.Options;

namespace Toolbox.Codetable.UnitTests.CodetabelServiceCollectionExtensionsTests
{
    public class AddCodetabelDiscoveryTests
    {
        [Fact]
        private void ControllerCodetabelProviderIsGeregistreerd()
        {
            var services = new ServiceCollection();
            services.AddCodetabelDiscovery(new CodetabelDiscoveryOptions());

            var registrations = services.Where(sd => sd.ServiceType == typeof(ICodetabelProvider) 
                                               && sd.ImplementationType == typeof(CodetabelProvider))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
        }

        [Fact]
        private void ControllerValueBuilderIsGeregistreerd()
        {
            var services = new ServiceCollection();
            services.AddCodetabelDiscovery(new CodetabelDiscoveryOptions());

            var registrations = services.Where(sd => sd.ServiceType == typeof(IValueBuilder)
                                               && sd.ImplementationType == typeof(ControllerValueBuilder))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
        }

        [Fact]
        private void CodetabelDiscoveryRouteBuilderIsGeregistreerd()
        {
            var services = new ServiceCollection();
            services.AddCodetabelDiscovery(new CodetabelDiscoveryOptions());

            var registrations = services.Where(sd => sd.ServiceType == typeof(ICodetabelDiscoveryRouteBuilder)
                                               && sd.ImplementationType == typeof(CodetabelDiscoveryRouteBuilder))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
        }

    }
}

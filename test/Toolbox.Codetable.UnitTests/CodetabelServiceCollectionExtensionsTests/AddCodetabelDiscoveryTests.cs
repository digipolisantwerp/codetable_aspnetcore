using System;
using System.Linq;
using Toolbox.Codetable.Internal;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Toolbox.DataAccess.Options;

namespace Toolbox.Codetable.UnitTests.CodetableServiceCollectionExtensionsTests
{
    public class AddCodetableDiscoveryTests
    {
        [Fact]
        private void ControllerCodetableProviderIsGeregistreerd()
        {
            var services = new ServiceCollection();
            services.AddCodetableDiscovery(new CodetableDiscoveryOptions());

            var registrations = services.Where(sd => sd.ServiceType == typeof(ICodetableProvider) 
                                               && sd.ImplementationType == typeof(CodetableProvider))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
        }

        [Fact]
        private void ControllerValueBuilderIsGeregistreerd()
        {
            var services = new ServiceCollection();
            services.AddCodetableDiscovery(new CodetableDiscoveryOptions());

            var registrations = services.Where(sd => sd.ServiceType == typeof(IValueBuilder)
                                               && sd.ImplementationType == typeof(ControllerValueBuilder))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
        }

        [Fact]
        private void CodetableDiscoveryRouteBuilderIsGeregistreerd()
        {
            var services = new ServiceCollection();
            services.AddCodetableDiscovery(new CodetableDiscoveryOptions());

            var registrations = services.Where(sd => sd.ServiceType == typeof(ICodetableDiscoveryRouteBuilder)
                                               && sd.ImplementationType == typeof(CodetableDiscoveryRouteBuilder))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
        }

    }
}

using Digipolis.Codetable.Internal;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Xunit;

namespace Digipolis.Codetable.UnitTests.CodetableServiceCollectionExtensionsTests
{
    public class AddCodetableDiscoveryTests
    {
        [Fact]
        private void ControllerCodetableProviderIsGeregistreerd()
        {
            var services = new ServiceCollection();
            services.AddCodetableDiscovery(options => { });

            var registrations = services.Where(sd => sd.ServiceType == typeof(ICodetableProvider) 
                                               && sd.ImplementationType == typeof(CodetableProvider))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
        }

        [Fact]
        private void ControllerValueBuilderIsGeregistreerd()
        {
            var services = new ServiceCollection();
            services.AddCodetableDiscovery(optiont => { });

            var registrations = services.Where(sd => sd.ServiceType == typeof(IValueBuilder)
                                               && sd.ImplementationType == typeof(ControllerValueBuilder))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
        }

        [Fact]
        private void CodetableDiscoveryRouteBuilderIsGeregistreerd()
        {
            var services = new ServiceCollection();
            services.AddCodetableDiscovery(optiont => { });

            var registrations = services.Where(sd => sd.ServiceType == typeof(ICodetableDiscoveryRouteBuilder)
                                               && sd.ImplementationType == typeof(CodetableDiscoveryRouteBuilder))
                                        .ToArray();
            Assert.Equal(1, registrations.Count());
        }

    }
}

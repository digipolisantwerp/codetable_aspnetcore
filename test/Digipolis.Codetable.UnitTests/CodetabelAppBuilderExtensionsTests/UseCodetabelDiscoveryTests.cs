using Microsoft.AspNetCore.Builder.Internal;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Digipolis.Codetable.UnitTests.CodetableAppBuilderExtensionsTests
{
    public class UseCodetableDiscoveryTests
    {
        [Fact]
        private void CodetablesWordenGeladen()
        {
            var assembly = typeof(UseCodetableDiscoveryTests).GetTypeInfo().Assembly;

            var codetableProvider = new Mock<ICodetableProvider>();
            codetableProvider.Setup(ctp => ctp.Load(assembly)).Verifiable();

            var options = new CodetableDiscoveryOptions();
            options.ControllerAssembly = assembly;

            var serviceProvider = MockServiceProvider(codetableProvider: codetableProvider.Object, option: options);

            var appBuilder = new ApplicationBuilder(serviceProvider);
            appBuilder.UseCodetableDiscovery();

            codetableProvider.Verify();
        }

        [Fact]
        private void CodetablesLoadedWithCustomRoute()
        {
            var assembly = typeof(UseCodetableDiscoveryTests).GetTypeInfo().Assembly;

            var codetableProvider = new Mock<ICodetableProvider>();
            codetableProvider.Setup(ctp => ctp.Load(assembly)).Verifiable();

            var options = new CodetableDiscoveryOptions();
            options.ControllerAssembly = assembly;
            options.Route = "custom/Route";

            var serviceProvider = MockServiceProvider(codetableProvider: codetableProvider.Object, option: options);

            var appBuilder = new ApplicationBuilder(serviceProvider);
            appBuilder.UseCodetableDiscovery();

            codetableProvider.Verify();
        }

        [Fact]
        private void AssemblyNullInOptionsNeemtCallingAssembly()
        {
            var callingAssembly = typeof(UseCodetableDiscoveryTests).GetTypeInfo().Assembly;      // Voor de toolbox is de calling assembly de executing assembly van deze test

            var codetableProvider = new Mock<ICodetableProvider>();
            codetableProvider.Setup(ctp => ctp.Load(callingAssembly)).Verifiable();

            var codetableDiscoveryOptions = new CodetableDiscoveryOptions() { ControllerAssembly = callingAssembly };

            var serviceProvider = MockServiceProvider(codetableProvider: codetableProvider.Object, option: codetableDiscoveryOptions);

            var appBuilder = new ApplicationBuilder(serviceProvider);
            appBuilder.UseCodetableDiscovery();

            codetableProvider.Verify();
        }

        private IServiceProvider MockServiceProvider(ICodetableProvider codetableProvider = null, ICodetableDiscoveryRouteBuilder routeBuilder = null, CodetableDiscoveryOptions option = null)
        {
            var provider = codetableProvider == null ? Mock.Of<ICodetableProvider>() : codetableProvider;
            var builder = routeBuilder == null ? Mock.Of<ICodetableDiscoveryRouteBuilder>() : routeBuilder;
            var actionDescriptorsProvider = MockActionDescriptorsProvider();
            
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(svp => svp.GetService(typeof(ICodetableProvider))).Returns(provider);
            serviceProvider.Setup(svp => svp.GetService(typeof(ICodetableDiscoveryRouteBuilder))).Returns(builder);
            serviceProvider.Setup(svp => svp.GetService(typeof(IActionDescriptorCollectionProvider))).Returns(actionDescriptorsProvider);
            serviceProvider.Setup(svp => svp.GetService(typeof(IOptions<CodetableDiscoveryOptions>)))
                .Returns(Options.Create(option));

            return serviceProvider.Object;
        }

        private IActionDescriptorCollectionProvider MockActionDescriptorsProvider()
        {
            var actionDescriptors = new List<ActionDescriptor>()
            {
                new ActionDescriptor()
                {
                    AttributeRouteInfo = new AttributeRouteInfo()
                    {
                        Template = "admin/codetable"
                    },
                    ActionConstraints = new List<IActionConstraintMetadata>(),
                    DisplayName = "Digipolis.Codetable.CodetableProviderController.GetAll"
                },
                new ActionDescriptor()
                {
                    AttributeRouteInfo = new AttributeRouteInfo()
                    {
                        Template = "api/component/{id}"
                    },
                    ActionConstraints = new List<IActionConstraintMetadata>(),
                    DisplayName = "Digipolis.component.componentprovidercontroller"
                },
            };

            var actionDescriptorsProvider = new Mock<IActionDescriptorCollectionProvider>();
            actionDescriptorsProvider.SetupGet(ad => ad.ActionDescriptors).Returns(new ActionDescriptorCollection(actionDescriptors, version: 1));

            return actionDescriptorsProvider.Object;
        }
    }
}

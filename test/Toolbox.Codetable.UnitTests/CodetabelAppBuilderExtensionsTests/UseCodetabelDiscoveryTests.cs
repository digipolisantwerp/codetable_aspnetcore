using System;
using System.Collections.Generic;
using System.Reflection;
using Toolbox.Codetable.Errors;
using Microsoft.AspNet.Builder.Extensions;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Routing;
using Moq;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNet.Mvc.Infrastructure;
using Microsoft.AspNet.Mvc.Abstractions;
using Microsoft.AspNet.Builder.Internal;
using Microsoft.Extensions.OptionsModel;
using Toolbox.Codetable.UnitTests.Utilities;

namespace Toolbox.Codetable.UnitTests.CodetableAppBuilderExtensionsTests
{
    public class UseCodetableDiscoveryTests
    {
        //[Fact]
        //private void OptionsNullRaisesArgumentNullException()
        //{
        //    IServiceCollection services = new ServiceCollection();
        //    var ex = Assert.Throws<ArgumentNullException>(() => services.AddCodetableDiscovery(null));
        //    Assert.Equal("setupAction", ex.ParamName);
        //}


        [Fact]
        private void CodetablesWordenGeladen()
        {
            var assembly = Assembly.GetExecutingAssembly();

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
            var assembly = Assembly.GetExecutingAssembly();

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
            var callingAssembly = Assembly.GetExecutingAssembly();      // Voor de toolbox is de calling assembly de executing assembly van deze test

            var codetableProvider = new Mock<ICodetableProvider>();
            codetableProvider.Setup(ctp => ctp.Load(callingAssembly)).Verifiable();

            var serviceProvider = MockServiceProvider(codetableProvider: codetableProvider.Object, option: new CodetableDiscoveryOptions());

            var appBuilder = new ApplicationBuilder(serviceProvider);
            appBuilder.UseCodetableDiscovery();

            codetableProvider.Verify();
        }

        private IServiceProvider MockServiceProvider(ICodetableProvider codetableProvider = null, ICodetableDiscoveryRouteBuilder routeBuilder = null, CodetableDiscoveryOptions option = null)
        {
            var provider = codetableProvider == null ? Mock.Of<ICodetableProvider>() : codetableProvider;
            var builder = routeBuilder == null ? Mock.Of<ICodetableDiscoveryRouteBuilder>() : routeBuilder;
            var actionDescriptorsProvider = MockActionDescriptorsProvider();
            var codetableDiscoveryOptions = option == null ? CodetableDiscoveryOptions.Default : option;
            
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(svp => svp.GetService(typeof(ICodetableProvider))).Returns(provider);
            serviceProvider.Setup(svp => svp.GetService(typeof(ICodetableDiscoveryRouteBuilder))).Returns(builder);
            serviceProvider.Setup(svp => svp.GetService(typeof(IActionDescriptorsCollectionProvider))).Returns(actionDescriptorsProvider);
            serviceProvider.Setup(svp => svp.GetService(typeof(IOptions<CodetableDiscoveryOptions>)))
                .Returns(Options.Create(option));

            return serviceProvider.Object;
        }

        private IActionDescriptorsCollectionProvider MockActionDescriptorsProvider()
        {
            var actionDescriptors = new List<ActionDescriptor>()
            {
                new ActionDescriptor()
                {
                    AttributeRouteInfo = new AttributeRouteInfo()
                    {
                        Template = "admin/codetable"
                    },
                    RouteConstraints = new List<RouteDataActionConstraint>()
                    {
                        new RouteDataActionConstraint(AttributeRouting.RouteGroupKey, "1"),
                    },
                    DisplayName = "Toolbox.Codetable.CodetableProviderController.GetAll"
                },
                new ActionDescriptor()
                {
                    AttributeRouteInfo = new AttributeRouteInfo()
                    {
                        Template = "api/component/{id}"
                    },
                    RouteConstraints = new List<RouteDataActionConstraint>()
                    {
                        new RouteDataActionConstraint(AttributeRouting.RouteGroupKey, "2"),
                    },
                    DisplayName = "Toolbox.component.componentprovidercontroller"
                },
            };

            var actionDescriptorsProvider = new Mock<IActionDescriptorsCollectionProvider>();
            actionDescriptorsProvider.SetupGet(ad => ad.ActionDescriptors).Returns(new ActionDescriptorsCollection((System.Collections.Generic.IReadOnlyList<Microsoft.AspNet.Mvc.Abstractions.ActionDescriptor>)actionDescriptors, version: 1));

            return actionDescriptorsProvider.Object;
        }
    }
}

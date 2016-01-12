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

namespace Toolbox.Codetable.UnitTests.CodetabelAppBuilderExtensionsTests
{
    public class UseCodetabelDiscoveryTests
    {
        [Fact]
        private void OptionsNullRaisesArgumentNullException()
        {
            IServiceCollection services = new ServiceCollection();
            var ex = Assert.Throws<ArgumentNullException>(() => services.AddCodetabelDiscovery(null));
            Assert.Equal("options", ex.ParamName);
        }


        [Fact]
        private void CodetabellenWordenGeladen()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var codetabelProvider = new Mock<ICodetabelProvider>();
            codetabelProvider.Setup(ctp => ctp.Load(assembly)).Verifiable();

            var serviceProvider = MockServiceProvider(codetabelProvider: codetabelProvider.Object, option:new CodetabelDiscoveryOptions(assembly));

            var appBuilder = new ApplicationBuilder(serviceProvider);
            appBuilder.UseCodetabelDiscovery();

            codetabelProvider.Verify();
        }

        [Fact]
        private void AssemblyNullInOptionsNeemtCallingAssembly()
        {
            var callingAssembly = Assembly.GetExecutingAssembly();      // Voor de toolbox is de calling assembly de executing assembly van deze test

            var codetabelProvider = new Mock<ICodetabelProvider>();
            codetabelProvider.Setup(ctp => ctp.Load(callingAssembly)).Verifiable();

            var serviceProvider = MockServiceProvider(codetabelProvider: codetabelProvider.Object, option: new CodetabelDiscoveryOptions(null));

            var appBuilder = new ApplicationBuilder(serviceProvider);
            appBuilder.UseCodetabelDiscovery();

            codetabelProvider.Verify();
        }

        private IServiceProvider MockServiceProvider(ICodetabelProvider codetabelProvider = null, ICodetabelDiscoveryRouteBuilder routeBuilder = null, CodetabelDiscoveryOptions option = null)
        {
            var provider = codetabelProvider == null ? Mock.Of<ICodetabelProvider>() : codetabelProvider;
            var builder = routeBuilder == null ? Mock.Of<ICodetabelDiscoveryRouteBuilder>() : routeBuilder;
            var actionDescriptorsProvider = MockActionDescriptorsProvider();
            
            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(svp => svp.GetService(typeof(ICodetabelProvider))).Returns(provider);
            serviceProvider.Setup(svp => svp.GetService(typeof(ICodetabelDiscoveryRouteBuilder))).Returns(builder);
            serviceProvider.Setup(svp => svp.GetService(typeof(IActionDescriptorsCollectionProvider))).Returns(actionDescriptorsProvider);
            serviceProvider.Setup(svp => svp.GetService(typeof(CodetabelDiscoveryOptions))).Returns(option);
            

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
                        Template = "admin/codetabel"
                    },
                    RouteConstraints = new List<RouteDataActionConstraint>()
                    {
                        new RouteDataActionConstraint(AttributeRouting.RouteGroupKey, "1"),
                    },
                    DisplayName = "Toolbox.Codetable.codetabelprovidercontroller"
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

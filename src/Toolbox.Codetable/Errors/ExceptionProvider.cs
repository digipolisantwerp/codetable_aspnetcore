using System;

namespace Toolbox.Codetable.Errors
{
    internal class ExceptionProvider
    {
        public static Exception CodetableProviderNotRegistered()
        {
            return new InvalidOperationException(ErrorMessages.ProviderNotRegistered);
        }

        public static Exception RouteBuilderNotRegistered()
        {
            return new InvalidOperationException(ErrorMessages.RouteBuilderNotRegistered);
        }

        public static Exception MvcNotRegisteredNoActionDescriptorsProvider()
        {
            return new InvalidOperationException(ErrorMessages.MvcNotRegisteredNoActionsDescriptorProvider);
        }

        public static Exception MvcNotRegisteredNoControllers()
        {
            return new InvalidOperationException(ErrorMessages.MvcNotRegisteredNoControllers);
        }
    }
}

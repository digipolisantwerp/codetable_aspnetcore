using System;

namespace Toolbox.Codetable.Errors
{
    internal class ExceptionProvider
    {
        public static Exception CodetabelProviderNietGeregistreerd()
        {
            return new InvalidOperationException(ErrorMessages.ProviderNietGeregistreerd);
        }

        public static Exception RouteBuilderNietGeregistreerd()
        {
            return new InvalidOperationException(ErrorMessages.RouteBuilderNietGeregistreerd);
        }

        public static Exception MvcNietGeregistreerdGeenActionDescriptorsProvider()
        {
            return new InvalidOperationException(ErrorMessages.MvcNietGeregistreerdGeenActionDescriptorsProvider);
        }

        public static Exception MvcNietGeregistreerdGeenControllers()
        {
            return new InvalidOperationException(ErrorMessages.MvcNietGeregistreerdGeenControllers);
        }
    }
}

using System;

namespace Toolbox.Codetable.Errors
{
    internal class ErrorMessages
    {
        public const string MaxLengthVeld = "{0} mag niet langer zijn dan {1}.";
        public const string MvcNietGeregistreerdGeenActionDescriptorsProvider = "De ActionDescriptorsProvider is nog niet geregistreerd. Wordt app.UseCodetabelDiscovery aangeroepen na app.UseMvc?";
        public const string MvcNietGeregistreerdGeenControllers = "De controllers zijn nog niet geregistreerd. Wordt app.UseCodetabelDiscovery aangeroepen na app.UseMvc?";
        public const string ProviderNietGeregistreerd = "De ICodetabelProvider is niet geregistreerd. Wordt 'services.AddCodetabelDiscovery' aangeroepen in de ConfigureServices method van de Startup class?";
        public const string RouteBuilderNietGeregistreerd = "De ICodetabelDiscoveryRouteBuilder is niet geregistreerd. Wordt 'services.AddCodetabelDiscovery' aangeroepen in de ConfigureServices method van de Startup class?";
        public const string VerplichtVeld = "{0} is verplicht.";
    }
}

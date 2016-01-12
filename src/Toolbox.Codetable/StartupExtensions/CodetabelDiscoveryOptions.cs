using System;
using System.Linq;
using System.Reflection;
using Toolbox.Common.Helpers;

namespace Toolbox.Codetable
{
    public class CodetabelDiscoveryOptions
    {
        public CodetabelDiscoveryOptions(Assembly controllerAssembly = null)
        {
            if ( controllerAssembly == null )
                ControllerAssembly = FindControllerAssembly();
            else
                ControllerAssembly = controllerAssembly;
            this.Route = Routes.CodetabelProviderController;
        }

        /// <summary>
        /// De route waar de lijst van codetabellen kan opgevraagd worden (default = '/admin/codetabel').
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        /// De assembly waar de codetabel controllers gedefinieerd zijn (default = de assembly waar de Startup class gevonden wordt).
        /// </summary>
        public Assembly ControllerAssembly { get; private set; }

        /// <summary>
        /// Default Route = 'admin/codetabel' en default Assembly is diegene waar de Startup class in gevonden wordt.
        /// </summary>
        public static CodetabelDiscoveryOptions Default {  get { return new CodetabelDiscoveryOptions(); } }

        private Assembly FindControllerAssembly()
        {
            var startup = ReflectionHelper.GetTypesFromAppDomain("startup");
            //if ( startup.Count() != 1 ) return;   // ToDo (SVB) :  < 1
            var controllerAssembly = startup.First().Assembly;
            return controllerAssembly;
        }
    }
}

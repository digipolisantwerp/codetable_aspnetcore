using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Toolbox.Codetable;
using WebTest.Entities;

namespace WebTest.Controllers
{
    [Route("api/RegistrationType")]
    [CodetableController]
    public class RegistrationTypeController : CodetableControllerBase<RegistrationType, WebTest.Models.RegistrationType>
    {
        public RegistrationTypeController(IServiceCollection collection, ILogger<RegistrationTypeController> logger) : base(collection, logger)
        {
        }
    }
}

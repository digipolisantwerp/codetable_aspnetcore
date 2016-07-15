using Digipolis.Codetable;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

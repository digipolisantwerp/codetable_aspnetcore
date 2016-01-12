using System;
using System.Linq;
using Toolbox.Codetable.Models;
using Microsoft.AspNet.Mvc;

namespace Toolbox.Codetable
{
    /// <summary>
    /// Controller die de lijst van codetabellen via discovery publiek maakt.
    /// </summary>
    [Route(Routes.CodetabelProviderController)]
    public class CodetabelProviderController : Controller
    {
        public CodetabelProviderController(ICodetabelProvider provider)
        {
            _provider = provider;
        }

        private readonly ICodetabelProvider _provider;

        [HttpGet("")]
        public IActionResult GetAll()
        {
            try
            {
                var codetabellen = _provider.Codetabellen;
                var models = ( from ct in codetabellen select new CodetabelInfo() { Naam = ct.Naam, Route = ct.Route } ).ToList();
                return new ObjectResult(models) { StatusCode = 200 };
            }
            catch ( Exception ex )
            {
                var error = new Error(ex.Message);
                return new ObjectResult(error) { StatusCode = 500 };
            }
        }
    }
}

using System;
using System.Linq;
using Toolbox.Codetable.Models;
using Microsoft.AspNet.Mvc;

namespace Toolbox.Codetable
{
    /// <summary>
    /// Controller that makes the list of codetables public via discovery.
    /// </summary>
    [Route(Routes.CodetableProviderController)]
    public class CodetableProviderController : Controller
    {
        public CodetableProviderController(ICodetableProvider provider)
        {
            _provider = provider;
        }

        private readonly ICodetableProvider _provider;

        [HttpGet("")]
        public IActionResult GetAll()
        {
            try
            {
                var codetables = _provider.Codetables;
                var models = ( from ct in codetables select new CodetableInfo() { Name = ct.Name, Route = ct.Route } ).ToList();
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

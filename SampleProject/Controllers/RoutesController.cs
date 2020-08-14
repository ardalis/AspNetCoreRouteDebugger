using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace RouteDebugging.Controllers
{
    [Route("[controller]/[action]")]
    public class Routes2Controller : Controller
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public Routes2Controller(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            this._actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        [HttpGet]
        [HttpPut]
        public IActionResult Index()
        {
            var routes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.Select(x => new {
                    Controller = x.RouteValues["Controller"],
                    Action = x.RouteValues["Action"],
                    Parameters = x.Parameters.Select(param => new
                    {
                        Name = param.Name,
                        Type = param.ParameterType.Name,
                    }),
                    Template = x.AttributeRouteInfo?.Template,
                    Name = x.AttributeRouteInfo?.Name,
                    HttpMethods = string.Join(", ", x.ActionConstraints?.OfType<HttpMethodActionConstraint>().First().HttpMethods ?? new List<string>()),
                }).OrderBy(x => x.Controller).ToList();

            return Json(routes, new JsonSerializerOptions
            {
                WriteIndented = true,
            });
        }
    }
}

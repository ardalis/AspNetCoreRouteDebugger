﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
                Action = x.RouteValues["Action"],
                Parameters = x.Parameters.Select(param => new
                {
                    Name = param.Name,
                    Type = param.ParameterType.Name,
                }),
                Controller = x.RouteValues["Controller"],
                Name = x.AttributeRouteInfo?.Name,
                Template = x.AttributeRouteInfo?.Template,
                Contraint = x.ActionConstraints
            }).OrderBy(x => x.Controller).ToList();
            return Ok (routes);
        }
    }
}

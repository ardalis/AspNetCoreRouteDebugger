using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using static RouteDebugging.Pages.RoutesModel;

namespace RouteDebugging.Controllers
{
    [Route("[controller]/[action]")]
    public class RoutesController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public RoutesController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        [HttpGet("/RoutesController")]
        public IActionResult Index()
        {
            var pageRoutes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.OfType<PageActionDescriptor>()
        .Select(x => new RouteInfo
        {
            Action = "",
            Controller = x.DisplayName,
            Name = x.AttributeRouteInfo?.Name,
            Template = x.AttributeRouteInfo?.Template,
            Constraint = x.ActionConstraints == null ? "" : JsonSerializer.Serialize(x.ActionConstraints),
            RouteValues = string.Join(',', x.RouteValues)
        })
    .OrderBy(r => r.Template);

            var viewRoutes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.OfType<ControllerActionDescriptor>()
                    .Select(x => new RouteInfo
                    {
                        Action = x.RouteValues["Action"],
                        Controller = x.RouteValues["Controller"],
                        Name = x.AttributeRouteInfo?.Name,
                        Template = x.AttributeRouteInfo?.Template,
                        Constraint = x.ActionConstraints == null ? "" : JsonSerializer.Serialize(x.ActionConstraints),
                    })
                .OrderBy(r => r.Template);

            var routes = pageRoutes.Concat(viewRoutes).ToList();

            return Json(routes, new JsonSerializerOptions
            {
                WriteIndented = true,
            });
        }
    }
}

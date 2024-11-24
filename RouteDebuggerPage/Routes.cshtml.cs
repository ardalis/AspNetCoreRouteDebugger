using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Text.Json;

namespace RouteDebugging.Pages;

public class RoutesModel : PageModel
{
  private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

  public RoutesModel(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
  {
    this._actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
  }

  public List<RouteInfo> Routes { get; set; } = new();

  public void OnGet()
  {
    // DisplayName
    var PageRoutes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.OfType<PageActionDescriptor>()
            .Select(x => new RouteInfo
            {
              Action = "",
              Controller = x.DisplayName ?? string.Empty,
              Name = x.AttributeRouteInfo?.Name ?? string.Empty,
              Template = x.AttributeRouteInfo?.Template ?? string.Empty,
              Constraint = x.ActionConstraints == null ? "" : JsonSerializer.Serialize(x.ActionConstraints),
              RouteValues = string.Join(',', x.RouteValues)
            })
        .OrderBy(r => r.Template)
        .ToList();

    var ViewRoutes = _actionDescriptorCollectionProvider.ActionDescriptors.Items.OfType<ControllerActionDescriptor>()
            .Select(x => new RouteInfo
            {
              Action = x.RouteValues["Action"] ?? string.Empty,
              Controller = x.RouteValues["Controller"] ?? string.Empty,
              Name = x.AttributeRouteInfo?.Name ?? string.Empty,
              Template = x.AttributeRouteInfo?.Template ?? string.Empty,
              Constraint = x.ActionConstraints == null ? "" : JsonSerializer.Serialize(x.ActionConstraints),
            })
        .OrderBy(r => r.Template)
        .ToList();

    Routes = PageRoutes.Concat(ViewRoutes).ToList();
  }

  public class RouteInfo
  {
    public string Template { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Controller { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string Constraint { get; set; } = string.Empty;
    public string RouteValues { get; set; } = string.Empty;
  }
}

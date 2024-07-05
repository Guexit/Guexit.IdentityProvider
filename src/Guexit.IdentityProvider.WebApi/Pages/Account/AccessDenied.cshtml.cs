using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace Guexit.IdentityProvider.WebApi.Pages.Account;

public class AccessDeniedModel : PageModel
{
    private readonly IStringLocalizer<AccessDeniedModel> _localizer;

    public AccessDeniedModel(IStringLocalizer<AccessDeniedModel> localizer)
    {
        _localizer = localizer;
    }

    public void OnGet()
    {
    }
}
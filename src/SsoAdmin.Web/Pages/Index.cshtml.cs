using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SsoAdmin.Web.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnGet() => RedirectToPage("/Usuarios/Index");
}

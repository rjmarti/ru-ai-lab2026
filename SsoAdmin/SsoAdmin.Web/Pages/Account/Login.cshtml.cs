using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SsoAdmin.Application.DTOs;
using SsoAdmin.Application.UseCases;
using System.Security.Claims;

namespace SsoAdmin.Web.Pages.Account;

public class LoginModel : PageModel
{
    private readonly ILoginUseCase _loginUseCase;

    public LoginModel(ILoginUseCase loginUseCase)
    {
        _loginUseCase = loginUseCase;
    }

    [BindProperty]
    public string Username { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }

    public IActionResult OnGet()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToPage("/Usuarios/Index");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl)
    {
        bool valid = await _loginUseCase.ExecuteAsync(new LoginRequest(Username, Password));
        if (!valid)
        {
            ErrorMessage = "Usuario o contraseña incorrectos.";
            return Page();
        }

        List<Claim> claims = [new(ClaimTypes.Name, Username)];
        ClaimsIdentity identity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        ClaimsPrincipal principal = new(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        return LocalRedirect(returnUrl ?? "/Usuarios");
    }
}

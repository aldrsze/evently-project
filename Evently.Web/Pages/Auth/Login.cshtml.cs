using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Evently.Core.Services;

namespace Evently.Web.Pages.Auth;

public class LoginModel : PageModel
{
    [BindProperty]
    public string Username { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Please enter both username and password.";
            return Page();
        }

        string identifier = Username.Trim();
        bool isValid = AuthService.VerifyAuth(identifier, Password);
        if (!isValid)
        {
            ErrorMessage = "Invalid username or password.";
            return Page();
        }

        if (!AuthService.IsApproved(identifier))
        {
            ErrorMessage = "Your account is waiting for admin approval.";
            return Page();
        }

        string actualUsername = AuthService.GetUsername(identifier) ?? identifier;
        string role = AuthService.GetRole(identifier) ?? "STAFF";

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, actualUsername),
            new(ClaimTypes.Role, role)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return RedirectToPage("/CheckIn/Scan");
    }
}

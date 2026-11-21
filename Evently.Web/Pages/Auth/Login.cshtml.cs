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

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Please enter both username and password.";
            return Page();
        }

        bool isValid = AuthService.VerifyAuth(Username.Trim(), Password);
        if (!isValid)
        {
            ErrorMessage = "Invalid username or password.";
            return Page();
        }

        // Store staff session / cookie or redirect to scan page
        return RedirectToPage("/CheckIn/Scan");
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Evently.Core.Services;

namespace Evently.Web.Pages.Auth;

public class SignupModel : PageModel
{
    [BindProperty]
    public string Username { get; set; } = string.Empty;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string FirstName { get; set; } = string.Empty;

    [BindProperty]
    public string LastName { get; set; } = string.Empty;

    [BindProperty]
    public string Birthdate { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    public string ConfirmPassword { get; set; } = string.Empty;

    public string? ErrorMessage { get; set; }

    public string? SuccessMessage { get; set; }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        string username = (Username ?? "").Trim();
        string email = (Email ?? "").Trim();
        string firstName = (FirstName ?? "").Trim();
        string lastName = (LastName ?? "").Trim();
        string password = Password ?? "";
        string confirmPassword = ConfirmPassword ?? "";

        if (username == "" || email == "" || firstName == "" || lastName == "" || password == "")
        {
            ErrorMessage = "Fill in username, email, first name, last name, and password.";
            return Page();
        }

        if (password.Length < UserService.MinPasswordLength)
        {
            ErrorMessage = "Use at least " + UserService.MinPasswordLength + " characters for the password.";
            return Page();
        }

        if (password != confirmPassword)
        {
            ErrorMessage = "The passwords do not match.";
            return Page();
        }

        DateTime? birthdate = null;
        if (!string.IsNullOrWhiteSpace(Birthdate))
        {
            if (!DateTime.TryParse(Birthdate, out DateTime parsedBirthdate))
            {
                ErrorMessage = "Enter a valid birthdate.";
                return Page();
            }
            birthdate = parsedBirthdate.Date;
        }

        // all web signups are STAFF and wait for desktop admin approval
        bool added;
        try
        {
            added = UserService.AddUser(username, password, email, firstName, lastName, birthdate, "STAFF", false);
        }
        catch (Exception ex)
        {
            ErrorMessage = "Signup could not be saved. " + ex.Message;
            return Page();
        }

        if (!added)
        {
            ErrorMessage = "That username or email is already taken.";
            return Page();
        }

        SuccessMessage = "Signup received. An admin must approve your account before you can sign in.";

        Username = "";
        Email = "";
        FirstName = "";
        LastName = "";
        Birthdate = "";
        Password = "";
        ConfirmPassword = "";
        return Page();
    }
}

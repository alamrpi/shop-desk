using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopDesk.Domain.Entities;
using ShopDesk.UI.Models;

/// <summary>
/// Manages user authentication processes including registration, login, and logout.
/// </summary>
[AllowAnonymous]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<AccountController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AccountController"/> class.
    /// </summary>
    /// <param name="userManager">Service for managing users in the persistence store.</param>
    /// <param name="signInManager">Service for managing user sign-in operations.</param>
    /// <param name="logger">Service for logging.</param>
    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<AccountController> logger) // Inject ILogger
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    /// <summary>
    /// Displays the user registration page.
    /// </summary>
    /// <returns>A view result for the registration page.</returns>
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    /// <summary>
    /// Handles the submission of the user registration form.
    /// </summary>
    /// <param name="model">The registration details provided by the user.</param>
    /// <returns>
    /// A redirect to the home page upon successful registration and sign-in.
    /// Otherwise, returns the registration view with validation errors.
    /// </returns>
    [HttpPost]
    [ValidateAntiForgeryToken] 
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            _logger.LogInformation("Registration attempt for email {Email}.", model.Email);

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, FirstName = model.Name , PhoneNumber = model.Mobile};

            try
            {
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User account for {Email} created successfully.", user.Email);

                    // Sign in the user automatically after registration
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation("User {Email} signed in automatically after registration.", user.Email);

                    return RedirectToAction("Index", "Home");
                }

                // If registration fails, log the errors and add them to the model state.
                foreach (var error in result.Errors)
                {
                    _logger.LogWarning("Registration failed for {Email}. Reason: {ErrorDescription}", model.Email, error.Description);
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during registration for user {Email}.", model.Email);
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
            }
        }
        else
        {
            _logger.LogWarning("Registration attempt failed due to invalid model state for email {Email}.", model.Email);
        }

        return View(model);
    }

    /// <summary>
    /// Displays the user login page.
    /// </summary>
    /// <returns>A view result for the login page.</returns>
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    /// <summary>
    /// Handles the submission of the user login form.
    /// </summary>
    /// <param name="model">The login credentials provided by the user.</param>
    /// <returns>
    /// A redirect to the home page upon successful login.
    /// Otherwise, returns the login view with an error message.
    /// </returns>
    [HttpPost]
    [ValidateAntiForgeryToken] // Good practice to prevent CSRF attacks
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            _logger.LogInformation("Login attempt for user {Email}.", model.Email);
            try
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User {Email} logged in successfully.", model.Email);
                    return RedirectToAction("Index", "Home");
                }

                // Log the failed login attempt
                _logger.LogWarning("Invalid login attempt for user {Email}.", model.Email);
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred during login for user {Email}.", model.Email);
                ModelState.AddModelError(string.Empty, "An unexpected error occurred. Please try again.");
            }
        }
        else
        {
            _logger.LogWarning("Login attempt failed due to invalid model state for user {Email}.", model.Email);
        }

        return View(model);
    }

    /// <summary>
    /// Logs out the currently authenticated user.
    /// </summary>
    /// <returns>A redirect to the home page after logging out.</returns>
    [HttpPost]
    [ValidateAntiForgeryToken] // Good practice to prevent CSRF attacks
    public async Task<IActionResult> Logout()
    {
        // Get user name before logging out for a more descriptive log message
        var userName = User.Identity?.Name ?? "Unknown";

        _logger.LogInformation("User {UserName} is logging out.", userName);

        await _signInManager.SignOutAsync();

        _logger.LogInformation("User {UserName} logged out successfully.", userName);

        return RedirectToAction("Index", "Home");
    }
}
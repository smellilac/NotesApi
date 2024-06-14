using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notes.Identity.Models;

namespace Notes.Identity.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly IIdentityServerInteractionService _interaction;

    public AuthController(SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager,
        IIdentityServerInteractionService interaction) =>
        (_signInManager, _userManager, _interaction) = (signInManager, userManager, interaction);

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        var viewModel = new LoginViewModel 
        { 
            ReturnUrl = returnUrl 
        };
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var user = await _userManager.FindByNameAsync(model.UserName);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            return View(model);
        }
        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password,
            false, false);
        if (result.Succeeded)
        {
            return Redirect(model.ReturnUrl);
        }
        ModelState.AddModelError(string.Empty, "Login error");
        return View(model);
    }

    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        var registerModel = new RegisterViewModel 
        { 
            ReturnUrl = returnUrl 
        };
        return View(registerModel);
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = new AppUser
        { 
            UserName = model.UserName 
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Redirect(model.ReturnUrl);
        }
        ModelState.AddModelError(string.Empty, "Error occured");
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string logoutId)
    {
        await _signInManager.SignOutAsync();
        var logoutRequest = await _interaction.GetLogoutContextAsync(logoutId);
        return Redirect(logoutRequest.PostLogoutRedirectUri);
    }
}

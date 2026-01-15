using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ShahiiERP.Application.Features.Auth.Login;
using System.Security.Claims;

public class AccountController : Controller
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public IActionResult Login()
    {
        var subdomain = HttpContext.Items["subdomain"] as string;
        var pathSegment = HttpContext.Items["pathSegment"] as string;

        if (!string.IsNullOrWhiteSpace(subdomain) || !string.IsNullOrWhiteSpace(pathSegment))
            ViewBag.TenantMode = "Auto";
        else
            ViewBag.TenantMode = "Manual"; // Z mode

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginInputModel input)
    {
        var result = await _mediator.Send(new LoginCommand(
            input.UserName,
            input.Password,
            input.TenantCode));

        if (!result.Success)
        {
            ModelState.AddModelError("", result.Errors?.FirstOrDefault() ?? "Login failed");
            return View(input);
        }

        // Cookie auth for web portal
        var claims = new List<Claim>
        {
            new Claim("uid", result.Data.UserName),
            new Claim("role", result.Data.Role),
            new Claim("tenant", result.Data.TenantId.ToString())
        };

        var identity = new ClaimsIdentity(claims, "Cookies");
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("Cookies", principal);

        return RedirectToAction("Index", "Home");
    }
}

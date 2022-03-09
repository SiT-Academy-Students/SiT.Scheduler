namespace SiT.Scheduler.API.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/auth")]
public class AuthenticationController : ControllerBase
{
    [HttpGet("validate")]
    public IActionResult ValidateAuthentication() => this.Ok();
}
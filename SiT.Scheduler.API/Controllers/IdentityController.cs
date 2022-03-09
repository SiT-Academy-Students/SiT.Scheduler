namespace SiT.Scheduler.API.Controllers;

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    [HttpPost("ensure")]
    public async Task<IActionResult> EnsureAsync(Guid userId, CancellationToken cancellationToken)
    {
        var clientSecretCredential = new ClientSecretCredential("", "", "");
        var client = new GraphServiceClient(clientSecretCredential);

        var user = await client.Users[userId.ToString()].Request().Select(x => new { x.DisplayName, x.Id }).GetAsync(cancellationToken);

        return this.Ok();
    }
}
namespace SiT.Scheduler.API.Controllers;

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Identity;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using SiT.Scheduler.API.Extensions;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.OperativeModels.Prototype;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    [NotNull]
    private readonly IIdentityService _identityService;

    public IdentityController([NotNull] IIdentityService identityService)
    {
        this._identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    }

    [HttpPost("ensure")]
    public async Task<IActionResult> EnsureAsync(Guid userId, CancellationToken cancellationToken)
    {
        var clientSecretCredential = new ClientSecretCredential("c2a4d5d7-9781-4163-b707-64e90ea26408", "4517141b-1961-4765-a5a7-3f118df7baf5", ".2v7Q~D4rKOmH9UXPutw4DtZHovDORc.m0FZU");
        var client = new GraphServiceClient(clientSecretCredential);

        var user = await client.Users[userId.ToString()].Request().Select(x => new { x.DisplayName, x.Id }).GetAsync(cancellationToken);

        var identityPrototype = new IdentityPrototype(userId, user.DisplayName);
        var createUser = await this._identityService.CreateAsync(identityPrototype, cancellationToken);
        if (!createUser.IsSuccessful) return this.Error(createUser);

        return this.Ok();
    }
}
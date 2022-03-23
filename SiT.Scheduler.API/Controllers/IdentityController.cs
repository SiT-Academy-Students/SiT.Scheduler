namespace SiT.Scheduler.API.Controllers;

using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using SiT.Scheduler.API.Extensions;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.OperativeModels.Prototype;
using SiT.Scheduler.Organization.Contracts;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    [NotNull]
    private readonly IGraphConnector _graphConnector;

    [NotNull]
    private readonly IIdentityService _identityService;

    public IdentityController([NotNull] IGraphConnector graphConnector, [NotNull] IIdentityService identityService)
    {
        this._graphConnector = graphConnector ?? throw new ArgumentNullException(nameof(graphConnector));
        this._identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    }

    [HttpPost("ensure")]
    public async Task<IActionResult> EnsureAsync(Guid userId, CancellationToken cancellationToken)
    {
        var getExternalUser = await this._graphConnector.GetExternalIdentityAsync(userId, cancellationToken);
        if (!getExternalUser.IsSuccessful) return this.Error(getExternalUser);

        var externalIdentity = getExternalUser.Data;
        if (externalIdentity is null) return this.NotFound();

        var identityPrototype = new IdentityPrototype(externalIdentity.Id, externalIdentity.DisplayName);
        var createUser = await this._identityService.CreateAsync(identityPrototype, cancellationToken);
        if (!createUser.IsSuccessful) return this.Error(createUser);

        return this.Ok();
    }
}
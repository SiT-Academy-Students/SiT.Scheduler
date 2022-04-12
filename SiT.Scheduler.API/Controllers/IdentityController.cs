namespace SiT.Scheduler.API.Controllers;

using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiT.Scheduler.API.Extensions;
using SiT.Scheduler.API.ViewModels.Identity;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.OperativeModels.Prototype;
using SiT.Scheduler.Core.Options;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Organization.Contracts;

[ApiController]
[Route("api/identity")]
public class IdentityController : ControllerBase
{
    [NotNull]
    private readonly IGraphConnector _graphConnector;

    [NotNull]
    private readonly ITenantService _tenantService;

    [NotNull]
    private readonly IIdentityService _identityService;

    public IdentityController([NotNull] IGraphConnector graphConnector, [NotNull] ITenantService tenantService, [NotNull] IIdentityService identityService)
    {
        this._graphConnector = graphConnector ?? throw new ArgumentNullException(nameof(graphConnector));
        this._tenantService = tenantService ?? throw new ArgumentNullException(nameof(tenantService));
        this._identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    }

    [AllowAnonymous]
    [HttpPost("ensure")]
    public async Task<IActionResult> EnsureAsync(Guid userId, CancellationToken cancellationToken)
    {
        var entityExistsOptions = new QueryEntityOptions<Identity>();
        entityExistsOptions.AddFilter(i => i.Id == userId);
        var entityExists = await this._identityService.AnyAsync(ExternalRequirement.Default, cancellationToken, options: entityExistsOptions);
        if (!entityExists.IsSuccessful) return this.Error(entityExists);
        if (entityExists.Data) return this.Conflict();

        var getExternalUser = await this._graphConnector.GetExternalIdentityAsync(userId, cancellationToken);
        if (!getExternalUser.IsSuccessful) return this.Error(getExternalUser);

        var externalIdentity = getExternalUser.Data;
        if (externalIdentity is null) return this.NotFound();

        var tenantPrototype = new TenantPrototype(Name: GenerateTenantName(externalIdentity.PrincipalName), IsSystem: true);
        var createTenant = await this._tenantService.CreateAsync(tenantPrototype, cancellationToken);
        if (!createTenant.IsSuccessful) return this.Error(createTenant);

        var identityPrototype = new IdentityPrototype(externalIdentity.Id, createTenant.Data.EntityId, externalIdentity.DisplayName);
        var createUser = await this._identityService.CreateAsync(identityPrototype, cancellationToken);
        if (!createUser.IsSuccessful) return this.Error(createUser);

        var identityEnsuredViewModel = new IdentityEnsuredViewModel { TenantId = createTenant.Data.EntityId, IdentityId = createUser.Data.EntityId };
        return this.Ok(identityEnsuredViewModel);
    }

    private static string GenerateTenantName(string principalName) => $"{principalName}-{Guid.NewGuid()}";
}
namespace SiT.Scheduler.API.Middlewares;

using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using SiT.Scheduler.API.Middlewares.Extensions;
using SiT.Scheduler.Core.Contracts.Authentication;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Core.Contracts.Services;
using SiT.Scheduler.Core.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Utilities.OperationResults;

public class AuthenticationContextMiddleware
{
    private readonly RequestDelegate _next;

    public AuthenticationContextMiddleware([NotNull] RequestDelegate next)
    {
        this._next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public Task InvokeAsync(HttpContext httpContext)
    {
        if (httpContext is null) throw new ArgumentNullException(nameof(httpContext));

        var user = httpContext.User;
        if (user.Identity is null || !user.Identity.IsAuthenticated) return this._next(httpContext);

        var userIdClaimValue = user.Claims.FirstOrDefault(x => x.Type == ClaimConstants.ObjectId)?.Value;
        if (!Guid.TryParse(userIdClaimValue, out var userId) || userId == Guid.Empty) return httpContext.BadRequest("Invalid user id claim");

        return this.AuthenticateUserAsync(httpContext, userId);
    }

    private async Task AuthenticateUserAsync(HttpContext httpContext, Guid userId)
    {
        var error = await AuthenticateUserInternallyAsync(httpContext, userId);
        if (string.IsNullOrWhiteSpace(error))
            await this._next(httpContext);
        else
            await httpContext.BadRequest(error);
    }

    private static async Task<string> AuthenticateUserInternallyAsync(HttpContext httpContext, Guid userId)
    {
        var identityService = httpContext.RequestServices.GetRequiredService<IIdentityService>();
        var getIdentity = await identityService.GetByExternalIdAsync<IIdentityAuthenticationLayout>(ExternalRequirement.Default, userId, httpContext.RequestAborted);
        if (!getIdentity.IsSuccessful) return getIdentity.ExtractErrors();

        var identity = getIdentity.Data;
        if (identity is null) return "User not found";

        if (!identity.Tenants.Any()) return "User has no tenants";
        if (identity.Tenants.Count > 1) return "User has more than one tenant";
        var tenant = identity.Tenants.Single();

        var authenticationContext = httpContext.RequestServices.GetRequiredService<IAuthenticationContext>();
        var tenantContext = httpContext.RequestServices.GetRequiredService<ITenantContext>();

        authenticationContext.Authenticate(identity.ContextualLayout);
        tenantContext.SetTenant(tenant);

        return string.Empty;
    }
}

public static class AuthenticationContextMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthenticationContext([NotNull] this IApplicationBuilder builder)
    {
        if (builder is null) throw new ArgumentNullException(nameof(builder));
        return builder.UseMiddleware<AuthenticationContextMiddleware>();
    }
}
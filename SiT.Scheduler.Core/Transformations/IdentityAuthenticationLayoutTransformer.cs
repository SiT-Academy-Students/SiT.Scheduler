namespace SiT.Scheduler.Core.Transformations;

using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;
using SiT.Scheduler.Core.Contracts.Transformations;
using SiT.Scheduler.Core.OperativeModels.Layouts;
using SiT.Scheduler.Data.Models;

public class IdentityAuthenticationLayoutTransformer : IDataTransformer<Identity, IIdentityAuthenticationLayout>
{
    [NotNull]
    private readonly IDataTransformer<Identity, IIdentityContextualLayout> _authenticationLayoutTransformer;
    [NotNull]
    private readonly IDataTransformer<Tenant, ITenantContextualLayout> _tenantLayoutTransformer;

    public IdentityAuthenticationLayoutTransformer([NotNull] IDataTransformer<Identity, IIdentityContextualLayout> authenticationLayoutTransformer, [NotNull] IDataTransformer<Tenant, ITenantContextualLayout> tenantLayoutTransformer)
    {
        this._authenticationLayoutTransformer = authenticationLayoutTransformer ?? throw new ArgumentNullException(nameof(authenticationLayoutTransformer));
        this._tenantLayoutTransformer = tenantLayoutTransformer ?? throw new ArgumentNullException(nameof(tenantLayoutTransformer));
    }

    public Expression<Func<Identity, IIdentityAuthenticationLayout>> Projection => this.BuildProjection();

    private Expression<Func<Identity, IIdentityAuthenticationLayout>> BuildProjection()
    {
        // return i => new IdentityContextualLayout(i.Id, new IdentityAuthenticationLayout(i.Id), i.Tenants.Project(this._tenantLayoutTransformer));
        return i => new IdentityAuthenticationLayout(i.Id, i.Project(this._authenticationLayoutTransformer), i.Tenants.Project(this._tenantLayoutTransformer));
    }
}
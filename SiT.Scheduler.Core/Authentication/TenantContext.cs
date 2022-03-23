namespace SiT.Scheduler.Core.Authentication;

using System;
using SiT.Scheduler.Core.Contracts.Authentication;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public class TenantContext : ITenantContext
{
    private readonly object _lockObj = new();

    public bool HasTenant { get; private set; }
    public ITenantContextualLayout Tenant { get; private set; }

    public void SetTenant(ITenantContextualLayout tenant)
    {
        lock (this._lockObj)
        {
            if (tenant is null) throw new ArgumentNullException(nameof(tenant));
            if (this.HasTenant) throw new InvalidOperationException("The tenant context was already populated");

            this.HasTenant = true;
            this.Tenant = tenant;
        }
    }
}
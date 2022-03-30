namespace SiT.Scheduler.Core.Authentication;

using System;
using SiT.Scheduler.Core.Contracts.Authentication;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public class AuthenticationContext : IAuthenticationContext
{
    private readonly object _lockObj = new();

    public bool IsAuthenticated { get; private set; }
    public IIdentityContextualLayout Identity { get; private set; }

    public void Authenticate(IIdentityContextualLayout identity)
    {
        lock (this._lockObj)
        {
            if (identity is null) throw new ArgumentNullException(nameof(identity));
            if (this.IsAuthenticated) throw new InvalidOperationException("Already authenticated");

            this.IsAuthenticated = true;
            this.Identity = identity;
        }
    }
}
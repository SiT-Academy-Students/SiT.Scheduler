namespace SiT.Scheduler.Core.Contracts.Authentication;

using JetBrains.Annotations;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public interface IAuthenticationContext
{
    bool IsAuthenticated { get; }
    IIdentityAuthenticationLayout Identity { get; }

    void Authenticate([NotNull] IIdentityAuthenticationLayout identity);
}
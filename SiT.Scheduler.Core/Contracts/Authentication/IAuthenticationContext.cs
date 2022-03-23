namespace SiT.Scheduler.Core.Contracts.Authentication;

using JetBrains.Annotations;
using SiT.Scheduler.Core.Contracts.OperativeModels.Layouts;

public interface IAuthenticationContext
{
    bool IsAuthenticated { get; }
    IIdentityContextualLayout Identity { get; }

    void Authenticate([NotNull] IIdentityContextualLayout identity);
}
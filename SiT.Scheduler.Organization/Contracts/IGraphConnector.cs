namespace SiT.Scheduler.Organization.Contracts;

using SiT.Scheduler.Utilities.OperationResults;

public interface IGraphConnector
{
    Task<IOperationResult<IExternalIdentity>> GetExternalIdentityAsync(Guid userId, CancellationToken cancellationToken);
}
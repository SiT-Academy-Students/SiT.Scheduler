namespace SiT.Scheduler.Core.Contracts.Services;

using System;
using System.Threading;
using System.Threading.Tasks;
using SiT.Scheduler.Core.Contracts.OperativeModels.ExternalRequirements;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;
using SiT.Scheduler.Core.Contracts.Options;
using SiT.Scheduler.Data.Models;
using SiT.Scheduler.Utilities.OperationResults;

public interface IIdentityService : IService<Identity, IDefaultExternalRequirement, IIdentityPrototype>
{
    Task<IOperationResult<TLayout>> GetByExternalIdAsync<TLayout>(IDefaultExternalRequirement externalRequirement, Guid externalId, CancellationToken cancellationToken, IQueryEntityOptions<Identity> options = null);
}
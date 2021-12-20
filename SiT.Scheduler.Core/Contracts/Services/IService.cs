namespace SiT.Scheduler.Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SiT.Scheduler.Core.Contracts.OperativeModels;
using SiT.Scheduler.Core.Contracts.Options;
using SiT.Scheduler.Data.Contracts.Models;
using SiT.Scheduler.Utilities.OperationResults;

public interface IService<TEntity, TExternalRequirement, TPrototype>
    where TEntity : class, IEntity
    where TExternalRequirement : class
    where TPrototype : class
{
    Task<IOperationResult<TLayout>> GetAsync<TLayout>(TExternalRequirement externalRequirement, Guid id, CancellationToken cancellationToken, IQueryEntityOptions<TEntity> options = null);
    Task<IOperationResult<IEnumerable<TLayout>>> GetManyAsync<TLayout>(TExternalRequirement externalRequirement, CancellationToken cancellationToken, IQueryEntityOptions<TEntity> options = null);
    Task<IOperationResult<ICreateEntityResult>> CreateAsync(TPrototype prototype, CancellationToken cancellationToken);
    Task<IOperationResult> UpdateAsync(Guid id, TPrototype prototype, CancellationToken cancellationToken);
}

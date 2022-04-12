namespace SiT.Scheduler.Data.Contracts.Repositories;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using SiT.Scheduler.Data.Contracts.Models;
using SiT.Scheduler.Utilities.OperationResults;

public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    Task<IOperationResult<bool>> AnyAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters, CancellationToken cancellationToken);
    Task<IOperationResult<TEntity>> GetAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters, CancellationToken cancellationToken);
    Task<IOperationResult<TLayout>> GetAsync<TLayout>(IEnumerable<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, TLayout>> projection, CancellationToken cancellationToken);
    Task<IOperationResult<IEnumerable<TEntity>>> GetManyAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters, CancellationToken cancellationToken);
    Task<IOperationResult<IEnumerable<TLayout>>> GetManyAsync<TLayout>(IEnumerable<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, TLayout>> projection, CancellationToken cancellationToken);
    Task<IOperationResult> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    Task<IOperationResult> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
}

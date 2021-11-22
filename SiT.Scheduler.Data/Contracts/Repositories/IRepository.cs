namespace SiT.Scheduler.Data.Contracts.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using SiT.Scheduler.Utilities.OperationResults;

    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<IOperationResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);
        Task<IOperationResult<TLayout>> GetAsync<TLayout>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TLayout>> projection, CancellationToken cancellationToken);
        Task<IOperationResult<IEnumerable<TEntity>>> GetManyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken);
        Task<IOperationResult<IEnumerable<TLayout>>> GetManyAsync<TLayout>(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, TLayout>> projection,CancellationToken cancellationToken);
        Task<IOperationResult> CreateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<IOperationResult> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    }
}

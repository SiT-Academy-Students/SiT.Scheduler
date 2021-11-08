namespace SiT.Scheduler.Data.Repositories
{
    using SiT.Scheduler.Data.Contracts.Repositories;
    using SiT.Scheduler.Data.Contracts.Models;
    using SiT.Scheduler.Utilitites.Errors;
    using SiT.Scheduler.Utilitites.OperationResults;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using SiT.Scheduler.Data.Models;
    using System.Linq;

    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private readonly SchedulerDbContext _schedulerDbContext;

        public Repository(SchedulerDbContext schedulerDbContext)
        {
            this._schedulerDbContext = schedulerDbContext ?? throw new ArgumentNullException(nameof(schedulerDbContext));
        }

        public async Task<IOperationResult> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult();

            operationResult.ValidateNotNull(entity);
            if (operationResult.IsSuccessful is false)
                return operationResult;

            try
            {
                await this._schedulerDbContext.AddAsync(entity, cancellationToken);
                await this._schedulerDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                var error = new ErrorFromException(e);
                operationResult.AddError(error);
            }

            return operationResult;
        }

        public Task<IOperationResult<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IOperationResult<IEnumerable<TEntity>>> GetManyAsync(Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<IOperationResult> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult();

            operationResult.ValidateNotNull(entity);
            if (operationResult.IsSuccessful is false)
                return operationResult;

            try
            {
                this._schedulerDbContext.Update(entity);
                await this._schedulerDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception e)
            {
                var error = new ErrorFromException(e);
                operationResult.AddError(error);
            }

            return operationResult;
        }
    }
}

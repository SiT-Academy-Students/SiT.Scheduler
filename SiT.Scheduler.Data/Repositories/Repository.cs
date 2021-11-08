using SiT.Scheduler.Data.Contracts.Models;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Utilities;
using SiT.Scheduler.Utilities.Errors;
using SiT.Scheduler.Utilities.OperationResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SiT.Scheduler.Data.Repositories
{
    public class Repository<TEntity> : IRepository<IEntity>
       where TEntity : class
    {
        private readonly SchedulerDbContext _schedulerDbContext;

        public Repository(SchedulerDbContext schedulerDbContext)
        {
            this._schedulerDbContext = schedulerDbContext ?? throw new ArgumentNullException(nameof(schedulerDbContext));
        }

        public async Task<IOperationResult> CreateAsync(IEntity entity, CancellationToken cancellationToken)
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
            catch(Exception e)
            {
                var error = new ErrorFromException(e);
                operationResult.AddError(error);
            }
           
            return operationResult;
        }

        public async Task<IOperationResult<IEntity>> GetAsync(Expression<Func<IEntity, bool>> filter, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<IEntity>();
            operationResult.ValidateNotNull(filter); ;
            if (operationResult.IsSuccessful is false)
                return operationResult;

            try
            {
                 var result = await this._schedulerDbContext.FindAsync<IEntity>(filter, cancellationToken);
                operationResult.Data = result;
            }
            catch(Exception e)
            {
                var error = new ErrorFromException(e);
                operationResult.AddError(error);
            }

            return operationResult;
        }

        public async Task<IOperationResult<IEnumerable<IEntity>>> GetManyAsync(Expression<Func<IEntity, bool>> filter, CancellationToken cancellationToken)
        {
            var operationResult = new OperationResult<IEnumerable<IEntity>>();
            operationResult.ValidateNotNull(filter); ;
            if (operationResult.IsSuccessful is false)
                return operationResult;

            try
            {
                var result = await this._schedulerDbContext.FindAsync<IEnumerable<IEntity>>(filter, cancellationToken);
                operationResult.Data = result;
            }
            catch (Exception e)
            {
                var error = new ErrorFromException(e);
                operationResult.AddError(error);
            }

            return operationResult;

        }            

        public Task<IOperationResult> UpdateAsync(IEntity entity, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}


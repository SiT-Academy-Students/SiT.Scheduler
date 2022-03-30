namespace SiT.Scheduler.Data.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SiT.Scheduler.Data.Contracts.Models;
using SiT.Scheduler.Data.Contracts.Repositories;
using SiT.Scheduler.Data.Extensions;
using SiT.Scheduler.Utilities.Errors;
using SiT.Scheduler.Utilities.OperationResults;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
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

    public async Task<IOperationResult<bool>> AnyAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<bool>();

        try
        {
            var result = await this._schedulerDbContext.Set<TEntity>().Filter(filters).AnyAsync(cancellationToken);
            operationResult.Data = result;
        }
        catch (Exception e)
        {
            var error = new ErrorFromException(e);
            operationResult.AddError(error);
        }

        return operationResult;
    }

    public async Task<IOperationResult<TEntity>> GetAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<TEntity>();

        try
        {
            var result = await this._schedulerDbContext.Set<TEntity>().Filter(filters).FirstOrDefaultAsync(cancellationToken);
            operationResult.Data = result;
        }
        catch (Exception e)
        {
            var error = new ErrorFromException(e);
            operationResult.AddError(error);
        }

        return operationResult;
    }

    public async Task<IOperationResult<TLayout>> GetAsync<TLayout>(IEnumerable<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, TLayout>> projection, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<TLayout>();

        try
        {
            var result = await this._schedulerDbContext.Set<TEntity>().Filter(filters).Select(projection).FirstOrDefaultAsync(cancellationToken);
            operationResult.Data = result;
        }

        catch (Exception e)
        {
            var error = new ErrorFromException(e);
            operationResult.AddError(error);
        }

        return operationResult;
    }

    public async Task<IOperationResult<IEnumerable<TEntity>>> GetManyAsync(IEnumerable<Expression<Func<TEntity, bool>>> filters, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<IEnumerable<TEntity>>();

        try
        {
            var result = await this._schedulerDbContext.Set<TEntity>().Filter(filters).ToListAsync(cancellationToken);
            operationResult.Data = result;
        }
        catch (Exception e)
        {
            var error = new ErrorFromException(e);
            operationResult.AddError(error);
        }

        return operationResult;
    }

    public async Task<IOperationResult<IEnumerable<TLayout>>> GetManyAsync<TLayout>(IEnumerable<Expression<Func<TEntity, bool>>> filters, Expression<Func<TEntity, TLayout>> projection, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult<IEnumerable<TLayout>>();

        try
        {
            var result = await this._schedulerDbContext.Set<TEntity>().Filter(filters).Select(projection).ToListAsync(cancellationToken);
            operationResult.Data = result;
        }

        catch (Exception e)
        {
            var error = new ErrorFromException(e);
            operationResult.AddError(error);
        }

        return operationResult;
    }

    public async Task<IOperationResult> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var operationResult = new OperationResult();

        operationResult.ValidateNotNull(entity);
        if (operationResult.IsSuccessful is false)
            return operationResult;

        try
        {
            var trackedEntity = this._schedulerDbContext.Set<TEntity>().Local.FirstOrDefault(x => x.Id == entity.Id);
            if (trackedEntity != null) this._schedulerDbContext.Entry(trackedEntity).State = EntityState.Detached;
            this._schedulerDbContext.Entry(entity).State = EntityState.Modified;

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

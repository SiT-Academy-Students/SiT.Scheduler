namespace SiT.Scheduler.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SiT.Scheduler.Core.Contracts.Options;

public class QueryEntityOptions<TEntity> : IQueryEntityOptions<TEntity>
{
    private readonly List<Expression<Func<TEntity, bool>>> _additionalFilters = new();

    public IReadOnlyCollection<Expression<Func<TEntity, bool>>> AdditionalFilters => this._additionalFilters.AsReadOnly();

    public bool AddFilter(Expression<Func<TEntity, bool>> filter)
    {
        if (filter is null) return false;

        this._additionalFilters.Add(filter);
        return true;
    }
}

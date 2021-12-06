namespace SiT.Scheduler.Core.Contracts.Transformations;
using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

public interface IDataTransformer<TEntity, TLayout>
{
    [NotNull]
    Expression<Func<TEntity, TLayout>> Projection { get; }
}

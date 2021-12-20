namespace SiT.Scheduler.Validation.Contracts;

using System.Threading;
using System.Threading.Tasks;
using SiT.Scheduler.Utilities.OperationResults;

public interface IEntityValidator<TEntity>
    where TEntity : class
{
    Task<IOperationResult> ValidateAsync(TEntity entity, CancellationToken cancellationToken);
}

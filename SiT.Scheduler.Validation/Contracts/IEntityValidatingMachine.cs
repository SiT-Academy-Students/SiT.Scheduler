namespace SiT.Scheduler.Validation.Contracts;

using System.Threading;
using System.Threading.Tasks;
using SiT.Scheduler.Utilities.OperationResults;

public interface IEntityValidatingMachine
{
    Task<IOperationResult> TriggerValidationAsync(CancellationToken cancellationToken);
}

namespace SiT.Scheduler.Utilities.OperationResults;

using System.Collections.Generic;
using SiT.Scheduler.Utilities.Errors;

public interface IOperationResult
{
    IReadOnlyCollection<IError> Errors { get; }
    bool IsSuccessful { get; }

    bool AddError(IError error);
}

public interface IOperationResult<out T> : IOperationResult
{
    T Data { get; }
}

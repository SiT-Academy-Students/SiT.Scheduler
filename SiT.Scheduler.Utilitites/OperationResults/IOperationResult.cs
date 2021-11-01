namespace SiT.Scheduler.Utilitites.OperationResults
{
    using SiT.Scheduler.Utilitites.Errors;
    using System.Collections.Generic;

    public interface IOperationResult
    {
        IReadOnlyCollection<IError> Errors { get; }
        bool IsSuccessful { get; }

        bool AddError(IError error);
    }

    public interface IOperationResult<T> : IOperationResult
    {
        T Data { get; }
    }
}

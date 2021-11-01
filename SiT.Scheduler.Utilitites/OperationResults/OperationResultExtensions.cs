using SiT.Scheduler.Utilitites.Errors;
using System;

namespace SiT.Scheduler.Utilitites.OperationResults
{
    public static class OperationResultExtensions
    {
        public static void ValidateNotNull<T>(this IOperationResult operationResult, T data, IError error = null)
            where T : class
        {
            if (operationResult is null)
                throw new ArgumentNullException(nameof(operationResult));

            if (data is null)
                operationResult.AddError(error ?? new ArgumentNullError());
        }
    }
}

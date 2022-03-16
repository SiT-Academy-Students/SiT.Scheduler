namespace SiT.Scheduler.Utilities.OperationResults;

using System;
using System.Text;
using JetBrains.Annotations;
using SiT.Scheduler.Utilities.Errors;

public static class OperationResultExtensions
{
    public static void ValidateNotNull<T>(this IOperationResult operationResult, T data, IError error = null)
        where T : class
    {
        if (operationResult is null)
            throw new ArgumentNullException(nameof(operationResult));

        if (data is null)
            operationResult.AddError(error ?? new InvalidArgumentError());
    }

    public static void ValidateNotDefault<T>(this IOperationResult operationResult, T data, IError error = null)
        where T : struct, IEquatable<T>
    {
        if (operationResult is null)
            throw new ArgumentNullException(nameof(operationResult));

        if (data.Equals(default))
            operationResult.AddError(error ?? new InvalidArgumentError());
    }

    public static TOperationResult AppendErrors<TOperationResult>(this TOperationResult operationResult, IOperationResult other)
        where TOperationResult : IOperationResult
    {
        if (operationResult is null)
            throw new ArgumentNullException(nameof(operationResult));

        if (other is null) return operationResult;

        foreach (var error in other.Errors)
            operationResult.AddError(error);

        return operationResult;
    }

    public static string ExtractErrors([NotNull] this IOperationResult operationResult)
    {
        if (operationResult is null) throw new ArgumentNullException(nameof(operationResult));

        var sb = new StringBuilder();
        foreach (var error in operationResult.Errors.OrEmptyIfNull().IgnoreNullValues()) sb.AppendLine(error.ErrorMessage);
        return sb.ToString();
    }
}

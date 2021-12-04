namespace SiT.Scheduler.Utilities.OperationResults;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiT.Scheduler.Utilities.Errors;

public class OperationResult : IOperationResult
{
    private readonly List<IError> _errors = new();

    public IReadOnlyCollection<IError> Errors => this._errors.AsReadOnly();

    public bool IsSuccessful => !this.Errors.Any();

    public bool AddError(IError error)
    {
        if (error is null) return false;

        this._errors.Add(error);
        return true;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var error in this._errors)
            sb.AppendLine(error.ErrorMessage);

        return sb.ToString();
    }
}

public class OperationResult<T> : OperationResult, IOperationResult<T>
{
    public T Data { get; set; }
}

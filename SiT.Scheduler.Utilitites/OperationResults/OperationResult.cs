namespace SiT.Scheduler.Utilitites.OperationResults
{
    using SiT.Scheduler.Utilitites.Errors;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class OperationResult : IOperationResult
    {
        private List<IError> _errors = new List<IError>();

        public IReadOnlyCollection<IError> Errors => this._errors.AsReadOnly();

        public bool IsSuccessful => this.Errors.Any() == false;

        public bool AddError(IError error)
        {
            if (error is null) return false;

            this._errors.Add(error);
            return true;
        }
    }

    public class OperationResult<T> : OperationResult, IOperationResult<T>
    {
        public T Data { get; set; }
    }
}

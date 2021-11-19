namespace SiT.Scheduler.Utilities.Errors
{
    using System;

    public class ErrorFromException : IError
    {
        private readonly Exception _exception;

        public ErrorFromException(Exception exception)
        {
            this._exception = exception ?? throw new ArgumentNullException(nameof(exception));
        }

        public string ErrorMessage => this._exception.Message;
    }
}

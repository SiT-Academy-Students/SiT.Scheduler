namespace SiT.Scheduler.Utilities.Errors;

using System;

public class StandardError : IError
{
    public StandardError(string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
            throw new ArgumentNullException(nameof(errorMessage));

        this.ErrorMessage = errorMessage;
    }

    public string ErrorMessage { get; }
}

namespace SiT.Scheduler.Utilities.Errors;

using SiT.Scheduler.Resources.LabelProviders;

public class InvalidArgumentError : IError
{
    public string ErrorMessage => GeneralErrors.InvalidArgument;
}

namespace SiT.Scheduler.Utilities.Errors
{
    using SiT.Scheduler.Resources.LabelProviders;

    public class ArgumentNullError : IError
    {
        public string ErrorMessage => GeneralErrors.ValueIsNull;
    }
}

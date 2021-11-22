namespace SiT.Scheduler.Utilities.Errors
{
    using SiT.Scheduler.Resources;

    public class ArgumentNullError : IError
    {
        public string ErrorMessage => GeneralErrors.ValueIsNull;
    }
}

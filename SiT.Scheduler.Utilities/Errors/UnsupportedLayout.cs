namespace SiT.Scheduler.Utilities.Errors;

using SiT.Scheduler.Resources.LabelProviders;

public class UnsupportedLayoutError : IError
{
    public string ErrorMessage => GeneralErrors.UnsupportedLayout;
}

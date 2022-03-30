namespace SiT.Scheduler.Utilities.Errors;

using SiT.Scheduler.Resources.LabelProviders;

public class MissingTenantContextError : IError
{
    public string ErrorMessage => GeneralErrors.MissingTenantContext;
}
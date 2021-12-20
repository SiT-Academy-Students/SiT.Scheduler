namespace SiT.Scheduler.Utilities.Errors;
using SiT.Scheduler.Resources.LabelProviders;

public class EntityDoesNotExistError : IError
{
    public string ErrorMessage => GeneralErrors.EntityDoesNotExist;
}

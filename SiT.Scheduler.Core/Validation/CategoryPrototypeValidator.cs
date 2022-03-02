namespace SiT.Scheduler.Core.Validation;

using FluentValidation;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;

public class CategoryPrototypeValidator : AbstractValidator<ICategoryPrototype>
{
    public CategoryPrototypeValidator()
    {
        this.RuleFor(x => x.Name).NotEmpty();
    }
}

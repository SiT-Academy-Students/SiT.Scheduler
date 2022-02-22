namespace SiT.Scheduler.Core.Validation;
using FluentValidation;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;

public class PerformerPrototypeValidator : AbstractValidator<IPerformerPrototype>
{
    public PerformerPrototypeValidator()
    {
        this.RuleFor(x => x.Name).NotEmpty();
    }
}

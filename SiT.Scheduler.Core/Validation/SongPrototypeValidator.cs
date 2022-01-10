namespace SiT.Scheduler.Core.Validation;
using FluentValidation;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;

public class SongPrototypeValidator : AbstractValidator<ISongPrototype>
{
    public SongPrototypeValidator()
    {
        this.RuleFor(x => x.Name).NotEmpty();
    }
}

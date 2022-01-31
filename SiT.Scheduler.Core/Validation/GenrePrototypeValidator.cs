namespace SiT.Scheduler.Core.Validation;
using FluentValidation;
using SiT.Scheduler.Core.Contracts.OperativeModels.Prototypes;

public class GenrePrototypeValidator : AbstractValidator<IGenrePrototype>
{
    public GenrePrototypeValidator()
    {
        this.RuleFor(x => x.Name).NotEmpty();
    }
}

using System;
using FluentValidation;
namespace SiT.Scheduler.Data.Validation
{
    public class SongValidator : AbstractValidator<Song>
    {
        public SongValidator()
        {
            this.RuleFor(x => x.Id).NotNull().WithMessage("Id cannot be empty");
            this.RuleFor(x => x.Name).NotEmpty().WithMessage("Must Enter Song Name").Length(4, 32).WithMessage("Song name must be between 4 and 32 symbols long");
            this.RuleFor(x => x.Author).NotEmpty().WithMessage("Must specify Song Author").Length(2, 60).WithMessage("Author's name must be between 2 and 60 symbols long");

        }
    }
}

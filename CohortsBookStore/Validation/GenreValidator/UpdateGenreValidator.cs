using CohortsBookStore.DTOs.GenreDtos;
using FluentValidation;

namespace CohortsBookStore.Validation.GenreValidator;

public class UpdateGenreValidator : AbstractValidator<UpdateGenreDto>
{
    public UpdateGenreValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
        RuleFor(x => x.Name)
            .NotEmpty().Length(3,30).WithMessage("Name must be between 3-30 characters !");
        RuleFor(x => x.IsActive)
            .NotEmpty();
    }
}
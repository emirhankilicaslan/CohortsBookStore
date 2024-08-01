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
            .NotEmpty().MinimumLength(2);
        RuleFor(x => x.IsActive)
            .NotEmpty();
    }
}
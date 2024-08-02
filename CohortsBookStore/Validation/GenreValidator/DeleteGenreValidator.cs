using FluentValidation;

namespace CohortsBookStore.Validation.GenreValidator;

public class DeleteGenreValidator : AbstractValidator<int>
{
    public DeleteGenreValidator()
    {
        RuleFor(x => x)
            .NotEmpty().GreaterThan(0);
    }
}
using FluentValidation;

namespace CohortsBookStore.Validation;

public class DeleteBookValidator : AbstractValidator<int>
{
    public DeleteBookValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .GreaterThan(0);
    }
}
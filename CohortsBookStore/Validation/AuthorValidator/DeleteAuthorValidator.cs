using FluentValidation;

namespace CohortsBookStore.Validation.AuthorValidator;

public class DeleteAuthorValidator : AbstractValidator<int>
{
    public DeleteAuthorValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .GreaterThan(0);
    }
}
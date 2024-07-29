using CohortsBookStore.DTO_s.BookDtos;
using FluentValidation;

namespace CohortsBookStore.Validation;

public class ByIdBookValidator : AbstractValidator<ByIdBookDto>
{
    public ByIdBookValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
    }
}
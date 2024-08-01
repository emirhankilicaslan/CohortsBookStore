using CohortsBookStore.DTOs.AuthorDtos;
using FluentValidation;

namespace CohortsBookStore.Validation.AuthorValidator;

public class UpdateAuthorValidator : AbstractValidator<UpdateAuthorDto>
{
    public UpdateAuthorValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().GreaterThan(0);
        RuleFor(x => x.Name)
            .NotEmpty().Length(3, 15);
        RuleFor(x => x.Surname)
            .NotEmpty().Length(3, 15);
        RuleFor(x => x.BirthDate)
            .NotEmpty().LessThan(DateTime.Now.Date);
    }
}
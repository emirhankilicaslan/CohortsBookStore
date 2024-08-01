using System.Data;
using CohortsBookStore.DTOs.AuthorDtos;
using FluentValidation;

namespace CohortsBookStore.Validation.AuthorValidator;

public class CreateAuthorValidator : AbstractValidator<CreateAuthorDto>
{
    public CreateAuthorValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().Length(3, 15);
        RuleFor(x => x.Surname)
            .NotEmpty().Length(3, 15);
        RuleFor(x => x.BirthDate)
            .NotEmpty().LessThan(DateTime.Now.Date);
    }
}
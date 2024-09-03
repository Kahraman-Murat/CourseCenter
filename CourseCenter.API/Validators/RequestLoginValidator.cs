using CourseCenter.DTO.DTOs.AuthDtos;
using FluentValidation;

namespace CourseCenter.API.Validators
{
    public class RequestLoginValidator : AbstractValidator<RequestLoginDto>
    {
        public RequestLoginValidator()
        {

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email Adresi alanı boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli bir Email Adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı boş bırakılamaz.");

        }
    }
}

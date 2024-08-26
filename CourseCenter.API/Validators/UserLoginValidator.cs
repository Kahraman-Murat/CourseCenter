using CourseCenter.DTO.DTOs.UserDtos;
using FluentValidation;

namespace CourseCenter.API.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator()
        {

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email Adresi alanı boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli bir Email Adresi giriniz.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı boş bırakılamaz.");

        }
    }
}

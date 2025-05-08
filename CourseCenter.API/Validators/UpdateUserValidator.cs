using CourseCenter.DTO.DTOs.UserDtos;
using FluentValidation;

namespace CourseCenter.API.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad Soyad alanı boş bırakılamaz.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı Adı alanı boş bırakılamaz.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email Adresi alanı boş bırakılamaz.")
                .EmailAddress().WithMessage("Geçerli bir Email Adresi giriniz.");

        }
    }
}

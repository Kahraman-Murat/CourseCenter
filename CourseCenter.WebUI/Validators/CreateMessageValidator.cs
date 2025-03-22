using CourseCenter.WebUI.DTOs.BannerDtos;
using CourseCenter.WebUI.DTOs.MessageDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class CreateMessageValidator : AbstractValidator<CreateMessageDto>
    {
        public CreateMessageValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Isim alanı boş bırakılamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email alanı boş bırakılamaz.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir Email Adresi girin.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Konu alanı boş bırakılamaz.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Mesaj Metın alanı boş bırakılamaz.");

        }
    }
}
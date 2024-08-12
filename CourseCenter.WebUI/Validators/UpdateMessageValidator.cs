using CourseCenter.WebUI.DTOs.BannerDtos;
using CourseCenter.WebUI.DTOs.MessageDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class UpdateMessageValidator : AbstractValidator<UpdateMessageDto>
    {
        public UpdateMessageValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Gecerli bir Email Adresi girin.");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x => x.Content).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

        }
    }
}
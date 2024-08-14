using CourseCenter.WebUI.DTOs.SubscriberDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class UpdateSubscriberValidator : AbstractValidator<UpdateSubscriberDto>
    {
        public UpdateSubscriberValidator()
        {

            RuleFor(x => x.Email).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Gecerli bir Email Adresi girin.");

        }
    }
}
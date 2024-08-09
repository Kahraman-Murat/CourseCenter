using CourseCenter.WebUI.DTOs.ContactDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class UpdateContactValidator : AbstractValidator<UpdateContactDto>
    {
        
        public UpdateContactValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Gecerli bir Email Adresi girin.");

            RuleFor(x => x.Phone).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.Address).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.MapUrl).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
        }
    }
}
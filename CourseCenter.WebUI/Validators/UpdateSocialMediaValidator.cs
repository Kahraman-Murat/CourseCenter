using CourseCenter.WebUI.DTOs.SocialMediaDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class UpdateSocialMediaValidator : AbstractValidator<UpdateSocialMediaDto>
    {
        public UpdateSocialMediaValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.Icon).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.Url).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

        }
    }
}
using CourseCenter.WebUI.DTOs.TeacherSocialMediaDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class CreateTeacherSocialMediaValidator : AbstractValidator<CreateTeacherSocialMediaDto>
    {
        public CreateTeacherSocialMediaValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.Icon).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.Url).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

        }
    }
}
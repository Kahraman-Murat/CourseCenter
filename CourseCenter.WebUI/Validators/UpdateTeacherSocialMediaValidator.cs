using CourseCenter.WebUI.DTOs.TeacherSocialMediaDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class UpdateTeacherSocialMediaValidator : AbstractValidator<UpdateTeacherSocialMediaDto>
    {
        public UpdateTeacherSocialMediaValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.Icon).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.Url).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

        }
    }
}
using CourseCenter.WebUI.DTOs.CourseCategoryDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class CreateCourseCategoryValidator : AbstractValidator<CreateCourseCategoryDto>
    {
        public CreateCourseCategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.Icon).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
        }
    }
}
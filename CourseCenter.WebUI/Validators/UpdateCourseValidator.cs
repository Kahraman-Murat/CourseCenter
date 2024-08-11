using CourseCenter.WebUI.DTOs.CourseDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class UpdateCourseValidator : AbstractValidator<UpdateCourseDto>
    {
        public UpdateCourseValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.ImageUrl).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.CourseCategoryId).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.Preis).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
        }
    }
}
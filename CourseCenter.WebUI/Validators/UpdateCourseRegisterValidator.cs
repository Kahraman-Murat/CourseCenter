using CourseCenter.WebUI.DTOs.CourseRegisterDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class UpdateCourseRegisterValidator : AbstractValidator<UpdateCourseRegisterDto>
    {
        public UpdateCourseRegisterValidator()
        {
            RuleFor(x => x.StudentId).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.CourseId).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
        }
    }
}
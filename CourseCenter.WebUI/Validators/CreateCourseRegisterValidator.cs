using CourseCenter.WebUI.DTOs.CourseRegisterDtos;
using FluentValidation;

namespace CourseCenter.WebUI.Validators
{
    public class CreateCourseRegisterValidator : AbstractValidator<CreateCourseRegisterDto>
    {
        public CreateCourseRegisterValidator()
        {
            //RuleFor(x => x.StudentId).NotEmpty().WithMessage("Bu alan bos birakilamaz.");

            RuleFor(x => x.CourseId).NotEmpty().WithMessage("Bu alan bos birakilamaz.");
        }
    }
}